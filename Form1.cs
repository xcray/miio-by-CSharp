using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miio
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Int32 tstamp=0;
        public bool waitfortoken = false;
        private System.Net.Sockets.UdpClient udpcMiio;
        System.Threading.Thread thrMiio;

        private void btEnc_Click(object sender, EventArgs e)
        {//文档有错，应为2131+包长+00000000+DID+stamp+token，然后是加密串，stamp每次从设备获取后+1
            string stamp = Convert.ToString(tstamp+1,16).PadLeft(8,'0');
            AddMessage(tbInfo, "加密时间戳：" + stamp);
            string encStr = Encrypt(tbJson.Text, tbToken.Text);
            string packLen = Convert.ToString((encStr.Length/2 + 32), 16).PadLeft(4,'0');
            string strHeader = "2131" + packLen + "00000000"+tbDid.Text + stamp;
            string md5sum=byteToHexStr(GetHexMD5(strHeader+tbToken.Text+encStr));
            tbEnc.Text = strHeader+md5sum+encStr;
        }
        private void btDec_Click(object sender, EventArgs e)
        {
            string tbEncs = tbEnc.Text.Replace(" ", "").Replace("\n","").Replace("\r", "");
            if (tbEncs.StartsWith("2131"))
            {
                AddMessage(tbInfo, "解密时间戳：" + tbEncs.Substring(24, 8));
                tbEncs = tbEncs.Remove(0, 64);
            }
            string decRes = Decrypt(tbEncs, tbToken.Text);
            tbJson.Text = decRes;
        }

        public static string Encrypt(string toEncrypt, string token)//加密
        {
            byte[] keyArray = GetHexMD5(token);
            byte[] ivArray = GetHexMD5(byteToHexStr(GetHexMD5(token))+token);
            byte[] toEncryptArray = Encoding.ASCII.GetBytes(toEncrypt);

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            rijndaelCipher.Key = keyArray;
            rijndaelCipher.IV = ivArray;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] cipherBytes = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return BitConverter.ToString(cipherBytes).Replace("-", "");
        }
        public static string Decrypt(string toDecrypt, string token)//解密
        {
            byte[] keyArray = GetHexMD5(token);
            byte[] ivArray = GetHexMD5(byteToHexStr(GetHexMD5(token)) + token);
            byte[] toDecryptArray = strToHexByte(toDecrypt);

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            rijndaelCipher.Key = keyArray;
            rijndaelCipher.IV = ivArray;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            try
            { byte[] cipherBytes = transform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                return UTF8Encoding.ASCII.GetString(cipherBytes);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            }
        // 发送UDP消息
        private void SendMessage(object obj)
        {
            string message = (string)obj;
            byte[] sendbytes = strToHexByte(message);
            IPAddress remoteIp = IPAddress.Parse(tbIP.Text);
            IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteIp, 54321);
            udpcMiio.Send(sendbytes, sendbytes.Length, remoteIpEndPoint);
        }
        public static byte[] GetHexMD5(string myHexString)
          {
              MD5 md5 = new MD5CryptoServiceProvider();
              byte[] fromData = strToHexByte(myHexString);
              byte[] targetData = md5.ComputeHash(fromData);
             return targetData;
         }
        public static byte[] GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            return targetData;
        }
        private static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace("\n", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        /// 字节数组转16进制字符串  
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            udpcMiio = new System.Net.Sockets.UdpClient();
            thrMiio = new System.Threading.Thread(ReceiveMessage);
            thrMiio.IsBackground = true;
            thrMiio.Start();

        }
        /// 接收数据
        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    if (udpcMiio.Client == null) return;
                    if (udpcMiio.Available <= 0) { System.Threading.Thread.Sleep(1000); continue; }
                    byte[] bytRecv = udpcMiio.Receive(ref remoteIpep);
                    string recmes =byteToHexStr(bytRecv);
                    AddMessage(tbInfo, "接收到：" + recmes);
                    if (waitfortoken && (recmes.StartsWith("2131002000000000")))
                    {
                        waitfortoken = false;
                        if ((recmes.Substring(32).StartsWith("FFFFFFFFFFFFFFFFFFFFF"))|| (recmes.Substring(32).StartsWith("000000000000000000000")))
                                {
                            AddMessage(tbInfo, "Token获取失败，需尝试其它获取方法（如米家App）");
                            tstamp = Int32.Parse(recmes.Substring(24, 8), System.Globalization.NumberStyles.HexNumber);
                            AddMessage(tbInfo, "获取到DID：" + recmes.Substring(16, 8) +"；和tstamp："+tstamp);
                            ShowMessage(tbDid, recmes.Substring(16, 8));
                            continue;
                                }
                        ShowMessage(tbToken, recmes.Substring(32));
                        ShowMessage(tbDid, recmes.Substring(16, 8));
                        tstamp = Int32.Parse(recmes.Substring(24, 8), System.Globalization.NumberStyles.HexNumber);
                        AddMessage(tbInfo, "Token & DID 已获取，别忘了检查确认。时间戳："+recmes.Substring(24,8));
                        continue;
                    }
                    tstamp = Int32.Parse(recmes.Substring(24, 8), System.Globalization.NumberStyles.HexNumber);
                    AddMessage(tbInfo,"接收到消息，解密为："+Decrypt(recmes.Substring(64),tbToken.Text)+"；时间戳："+ recmes.Substring(24, 8));
                }
                catch (Exception ex) { AddMessage(tbInfo, ex.Message); }
            }
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (tbToken.Text.Length<5) btToken_Click(sender, e);
            System.Threading.Thread.Sleep(600);
            if (tstamp<2) { AddMessage(tbInfo,"获取token后重新点击发送按钮"); return; }
            btEnc_Click(sender,e);
            SendMessage(tbEnc.Text);
        }

        private void btToken_Click(object sender, EventArgs e)
        {
            string iniHello = "21310020ffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
            SendMessage(iniHello);
            waitfortoken=true;
        }
        // 向TextBox中替换文本
        delegate void ShowMessageDelegate(TextBox txtbox, string message);
        private void ShowMessage(TextBox txtbox, string message)
        {
            if (txtbox.InvokeRequired)
            {
                ShowMessageDelegate showMessageDelegate = ShowMessage;
                txtbox.Invoke(showMessageDelegate, new object[] { txtbox, message });
            }
            else
            {
                txtbox.Text = message;
            }
        }
        // 向TextBox中添加文本
        delegate void AddMessageDelegate(TextBox txtbox, string message);
        private void AddMessage(TextBox txtbox, string message)
        {
            if (txtbox.InvokeRequired)
            {
                AddMessageDelegate addMessageDelegate = AddMessage;
                txtbox.Invoke(addMessageDelegate, new object[] { txtbox, message });
            }
            else
            {
                txtbox.Text += "\r\n" + message;
                txtbox.Select(txtbox.TextLength, 0);//光标定位到文本最后
                txtbox.ScrollToCaret();//滚动到光标
            }
        }

    }

}

