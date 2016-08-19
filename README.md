# MovieDesktop
###### 梦幻桌面

### 简介
这是一款使用C#开发的动态桌面小程序，可以将自己喜欢的视频设置成桌面，支持列表播放<br>
* 需要Windows Media Player 支持 *<br>
目前所支持的功能
* 播放列表
* 上一桌面
* 下一桌面
* 静音
* 暂停
* 停止

### 使用方法
在`F:\Programtempfile\`中新建一个`1.wpl`文件，编写Windows Media Player的播放列表，下面是一个示例：<br>
~~~~
<?wpl version="1.0"?>
<smil>
    <head>
        <meta name="Generator" content="Microsoft Windows Media Player -- 12.0.9600.17415"/>
        <meta name="ItemCount" content="1"/>
        <author/>
        <title>1</title>
    </head>
    <body>
        <seq>
            <media src="1.avi"/>
        </seq>
    </body>
</smil>
~~~~
*你也可以使用 Windows Media Player 自动生成该文件，详细方法参见 WMP 的保存播放列表的帮助*