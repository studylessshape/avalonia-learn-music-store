# Avalonia.MusicStore
来自 Avalonia 官方文档：[音乐商店应用](http://git.studylessshape.fun/studylessshape/avalonia-learn-music-store)

官方文档使用 ReactiveUI，个人不是很喜欢这种很重的框架，我尝试使用 CommunityToolkit.Mvvm 并结合 Microsoft.Extensions.DependenctInject 实现。

## 格式化插件
Visual Studio 插件市场中搜索 `XAML Styler`（[link](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler2022)），可以在保存时格式化 XML，挺好用的。

我的配置参考如下：

![xaml-styler-setting](./docs/assets/xaml-styler-setting.png)

主要修改了下面几个地方：

1. `Keep first attribute on same line`：保证第一个属性在同一行；
2. `Enable Atrribute Reordering`：属性排序；
3. `Put ending brackets on new line`：将结束括号放在新行（可以不能控制是否是自闭型）。

## 部分需要理解的点
官方文档中有部分看着无来由，也没有详细解释的点，学习时我将记录在下面。

### 1. 窗口样式（[link](https://docs.avaloniaui.net/zh-Hans/docs/tutorials/music-store-app/creating-a-modern-looking-window)）
#### TransparencyLevelHint
这个会令我比较疑惑，其中的值从何而来。

看到源码 [WindowTransparencyLevel.cs#L33](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/WindowTransparencyLevel.cs#L33)，这里理论上应该是将其默认具有的几种透明方式注入了，但是插件的代码提示中并没有。

`AcrylicBlur` 即是 33 行定义的一个字段。

#### 亚克力模糊效果中 `Panel` 应该在哪里设置
注意到我的项目结构和官方的不一致。

因为我创建是直接创建的跨平台项目，所以在桌面端，使用的是 `MainWindow`，网页端估计是 `MainView`，手机端可能也是 `MainView`。

而 `MainWindow` 中的内容部分，是直接使用了 `MainView`，所以出了需要直接设置到 `MainWindow` 的地方，其他的都可以在 `MainView` 中设置。