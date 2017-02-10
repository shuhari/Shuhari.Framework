TODO:
* FrameworkDatabase lazy create sessionFactory instead of startup
* DbEngine.ExecuteResourceScript and DbManagementCommandOptions can be removed

DONE:
* FIXED: IQuery<T>.Set unified
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute
* NEW: GZip.Compress/Decompress
* NEW: Encryption.SHA1
* REMOVED: CollectionUtil.FindBy/FindByName
* REMOVED: IDbEngine.ExecuteCommand
* REMOVED: TextReplacer
* NEW: DbSriptExecuteOptions
* NEW: CommandLine
* FrameworkDatabase use lazy creation of sessionFactory