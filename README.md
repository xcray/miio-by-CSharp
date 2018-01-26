# miio-by-C#

miio协议的中文补充信息，以及C#测试程序（电脑版米家App原型，模块之一：WiFi器件交互）。

首先感谢OpenMiHome的[miio-binary-protocol](https://github.com/OpenMiHome/mihome-binary-protocol/blob/master/doc/PROTOCOL.md)！

但是有些遗憾，其中的有些信息存在小的错误，有些重要的信息没有提供。

在尝试用C#编写一个智能插座控制小程序的过程中，踩过几个坑，也许值得更多人参考。

（注：这是我第一次在GitHub上开Repo，很多东西还不太明白，大家原谅）

1、关于加密解密用的IV，OpenMiHome中的说明存在错误（程序里是对的只是说明有误），应为 MD5(Key+Token)，即 MD5(MD5(Token)+Token)。

2、关于消息体中的md5sum字段，原说明也存在错误（计算时先用0填充，其实应该用token），虽然有大侠指出但说明依然不够清晰。应该先用：

（正确的头部（2131+包长）+ unkown（00000000）+ DID + stamp + token + 加密后的载荷）

构造出消息体，然后计算出整个消息体的MD5后，再用计算所得的md5替换掉其中的token字段，然后就可以发到设备侧了。

3、关于stamp，其实这个字段非常重要，但原说明未清晰描述。根据我用智能插座分析的结论，设备在插电后从0开始计算秒数，作为设备侧的计时。控制设备的指令中的stamp应与设备内的计时秒数（就是设备在收到InitialHello或握手消息后回复消息中的stamp）差不多才行（比如简单+1），否则设备会认为收到的指令间隔时间太长从而忽略。

   实际上，不管是手机上的客户端，还是那个rytilahti/python-miio，在向设备发出控制或查询指令前，都会先发一条InitialHello或握手消息（就是那串21310020FFFFFFFFFF...），从设备回复的Hello中获取到设备发出的stamp（当然，还可以同时获得token和DID），然后+1用作指令消息的stamp。

4、智能插座（基础版，V1）支持的指令载荷有（xxx为消息ID，整数，客户端自主控制，设备回复使用同样的ID以便客户端识别顺序）：

 - 状态读取：{"id":xxx,"method":"get_prop","params":["on","usb_on","temperature"]}，插座回复 {"result":[true,true,41],"id":xxx}，即220v开关状态、USB开关状态，以及温度。

 - 220v打开/关闭：{"id":xxx,"method":"set_on","params":[]} 或 {"id": 1, "method": "set_power", "params": ["on"]}，两种格式都可以，手机客户端用的是前者，python-miio用的是后者。220v关闭就是里面的“on”改为“off”。

 - USB打开/关闭：{"id":xxx,"method":"set_usb_on","params":[]}，同样关闭对应着“off”，这是手机客户端使用的，python-miio貌似不支持USB控制。

5、基础版插座（WiFi）我手上有2个版本：带USB的PlugV1和不带USB的Plug。其中PlugV1可以如此自动获取到token，不带USB的不行（只能通过备份文件获取）。

   其他设备如空气净化器（第一版）、多功能网关（第二版，lumi-gateway-v3）也可以获取到，扫地机器人不行。估计与采用的wifi模块版本有关系。

6、其他（比如网关支持的指令载荷等）参见wiki。
