# MovieDesktop
###### 梦幻桌面

### 简介
这是一款使用C#开发的动态桌面小程序，可以将自己喜欢的视频设置成桌面，支持列表播放<br>
**注意：需要Windows Media Player 支持** <br>
[演示视频](http://www.bilibili.com/video/av2304939/)<br>

目前所支持的功能
* 播放列表
* 上一桌面
* 下一桌面
* 静音
* 暂停
* 停止
* 自定义播放列表位置\[new\]

### 更新日志
2016年9月22日 V0.0.1.0Bate
1. 微调了右键菜单
2. 添加了自定义播放列表位置功能

### 使用方法

新建一个`*.wpl`文件，编写Windows Media Player的播放列表<br>

下面是一个示例：<br>
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
**你也可以使用 Windows Media Player 自动生成该文件，详细方法参见 WMP 的保存播放列表的帮助**

启动程序后右键托盘菜单--> `设置` --> `浏览wpl保存` --> `重启程序`

### 已知BUG
1. 桌面文件无法拖动
2. 部分系统找不到桌面
3. 桌面图标颜色为#000的区域缺失
4. 最右有1px的缝隙
5. 更换下一桌面时会自动恢复音量

### 实现方法
1. 窗口内放置一播放器，并将窗体设置成与屏幕大小相同
2. 将桌面的父窗体设置成程序窗口
3. 将程序窗口父窗体设置成桌面原父窗体

### 后记
由于自身技术限制，已经暂时搁置，如有更好的解决方案可以联系我

### License
GPLv3