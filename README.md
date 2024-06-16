【AK杂谈】大气层1.7.0后sigpatch签名补丁的三种解决办法

大气层1.7.0开始，SciresM大神删除KIP的加载功能，原来只是为了nogc卡槽保护，但是这个功能已集成在stratosphere.romfs内核中，通过stratosphere.ini调设定，不需要额外补丁。这个删除KIP的功能会影响FS补丁的加载，极限超频的loader.kip等一系列问题。

目前看来有三种方法解决这种办法：

（1）因为大气层三件套是atmosphere+hekate+sigpatch，启动系统的方法是四种，fusee大气层自动识别不能再加载KIP补丁，但是通过Hekate的fss0引导的真实（破解）系统，虚拟（破解）系统的KIP是通过bootloader/patch.ini实现，所以不受大气层1.7.0升级导致不能玩破解游戏的影响。

https://www.tekqart.com/thread-289271-1-1.html

左起1，2，3都是fss0引导，最右4是fusee引导。

（2）sigpatch可以是IPS加载，也可以是通过sys-patch的插件签名补丁的方法，目前原版sys-patch被删库，后续有大神接着开发，论坛有转载，相当于不需要sigpatch组件，直接在破解系统里sysmodule启动，通过Tesla选择是否开启sigpatch，这样就能玩破解游戏了。

sys-patch插件同时支持fusee引导和fss0引导，所以hekate_ipl.ini中的fss0引导不需要设置kip1patch=nosigchk，也不需要patches.ini。

所以这种三件套（也可以说四件套）=atmosphere+hekate+sys-patch+tesla，Tesla用于调用sys-patch的菜单。

莱莱大佬发布集成sys-patch插件的大气层整合包

https://www.tekqart.com/thread-391203-1-1.html

（3）lsp大佬编译大气层1.7.0的核心文件，atmosphere/package3，atmosphere/stratosphere.romfs，fusee.bin，可以把loader patch限制去掉，还可以把sigpatch的fs补丁内置。

https://www.tekqart.com/thread-382514-1-1.html

以上三种方法没区别，用哪种结果都一样，但是第一种作为以前的延续，使用起来相对简单些，也就是AK一直以来发布的可覆盖的大气层三件套文件

https://www.tekqart.com/thread-321617-1-1.html

可以覆盖，只是在Hekate_ipl.ini里不选择fusee引导（cfw auto），也是作为1.7.0大气层之后的解决玩破解游戏的方法。


Sigpatch又名大气层签名补丁，允许大气层系统运行破解游戏和前端软件。

ES Patch，FS Patch和Loader Patch是签名补丁，nfim ctest是跳过任天堂服务器验证。

（1）ES Patch在SD卡的atmosphere/exefs_patches/es_patches，对应SW系统，每次SW系统大版本升级会需要新增1个IPS。

建议保留旧的ES Patch，如果系统降级后需要玩破解游戏，就会需要对应旧的ES Patch，ES补丁允许你从Eshop商店dump出来的原版NSP（含压缩格式NSZ）游戏文件安装和正常运行。

（2）FS Patch在SD卡的atmosphere/kip_patches/fs_patches，对应SW系统，每次SW系统大版本升级会需要新增2个IPS（exfat和fat32）。

同上，建议保留旧的FS Patch，如果系统降级后需要玩破解游戏，就会需要对应旧的FS Patch，FS补丁允许你使用非原版NSP文件（NSP，NSZ，XCI，XCZ）的安装和正常运行，包括NRO插件转NSP格式，整合版XCI，NSP等格式。

（3）Loader Patch在SD卡的atmosphere/kip_patches/loader_patches，对应大气层package3，每次大气层版本升级需要新增1个IPS。

但旧的Loader Patch无用，可以删除，大气层向下兼容，系统降级不需要换大气层文件。

（4）nfim ctest在SD卡的atmosphere/exefs_patches/nfim_ctest，和SW系统有关，建议保留旧的nfim ctest。

（5）大气层系统分fss0引导（真实、虚拟、正版系统）和fusee引导（自动识别）。

fss0引导的FS Patch和Loader Patch在SD卡的bootloader/patches.ini，不同于fusee引导。

fss0引导的ES Patch和nfim ctest，与fusee引导一样，没有区别。
