Sigpatch又名大气层签名补丁，允许大气层系统运行破解游戏和前端软件。

ES Patch，FS Patch和Loader Patch是签名补丁，nfim ctest是跳过任天堂服务器验证。

（1）ES Patch在SD卡的atmosphere/exefs_patches/es_patches，对应SW系统，每次SW系统大版本升级会需要新增1个IPS。

建议保留旧的ES Patch，如果系统降级后需要玩破解游戏，就会需要对应旧的ES Patch。

（2）FS Patch在SD卡的atmosphere/kip_patches/fs_patches，对应SW系统，每次SW系统大版本升级会需要新增2个IPS（exfat和fat32）。

同上，建议保留旧的FS Patch，如果系统降级后需要玩破解游戏，就会需要对应旧的FS Patch。

（3）Loader Patch在SD卡的atmosphere/kip_patches/loader_patches，对应大气层package3，每次大气层版本升级需要新增1个IPS。

但旧的Loader Patch无用，可以删除，大气层向下兼容，系统降级不需要换大气层文件。

（4）nfim ctest在SD卡的atmosphere/exefs_patches/nfim_ctest，和SW系统有关，建议保留旧的nfim ctest。

（5）大气层系统分fss0引导（真实、虚拟、正版系统）和fusee引导（自动识别）。

fss0引导的FS Patch和Loader Patch在SD卡的bootloader/patches.ini，不同于fusee引导。

fss0引导的ES Patch和nfim ctest，与fusee引导一样，没有区别。
