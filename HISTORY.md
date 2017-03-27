**v0.1.13.0 (2017-03-27)**
* NEW: WPF HGridSplitter/VGridSplitter
* NEW: FileSystemExtensions.EnsureDirectory/WriteText/WriteBytes
* FIXED: FrameworkStrings use property instead of field to avoid NPE

**v0.1.12.0 (2017-03-23)**
* NEW: XmlSerializer support CDATA

**v0.1.11.0 (2017-03-16)**
* FIXED: FrameworkController.ExecuteJson clear model state
* FIXED: XmlSerializer.TypeFactory incorrect named
* NEW: WPF Form Grid

**v0.1.10.1 (2017-03-15)**
* FIXED: Directory.ForceDelete recursive

**v0.1.10.0 (2017-03-15)**
* NEW: ModelState.GetErrors/GetFirstError
* NEW: ExecuteJsonResult add default parameter for validation
* REFACTOR: ExecuteJsonResult renamed to ExecuteJson
* NEW: FileSystemExtensions

**v0.1.9.0 (2017-03-13)**
* REFACTOR: DTO class renamed to DTO
* REFACTOR: ResultDto.SetResult return self
* NEW: CollectionUtil.Safe
* NEW: StringParts
* NEW: Xml serialization support

**v0.1.8.0 (2017-03-06)**
* NEW: StopEnumerationException
* REFATOR: XmlUtil.SafeAttr renamed to GetAttr
* NEW: XmlUtil.SetAttr/AppendElement/ToXmlString
* NEW: TagExtensions

**v0.1.7.1 (2017-03-03)**
* NEW: Rename WPF namespace

**v0.1.7.0 (2017-03-02)**
* REFACTOR: Testing.NUnit split to Testing.Common/Testing.Mvc
* NEW: HBox/VBox control
* NEW: SimpleGrid control
* New: ElementExtensions

**v0.1.6.0 (2017-02-27)**
* REFACTOR: Framework downlevel to 4.5.2
* NEW: WPF library project

**v0.1.5.0 (2017-02-25)**
* REFACTOR: MappingFactory.MapEntitiesWithAnnonations set to public
* NEW: DbContextFactory

**v0.1.4.0 (2017-02-21)**
* NEW: StreamDecorator expose InnerStream
* NEW: AuthenticationResultDTO
* REFACTOR: UserManager.Signin return user info
* REFACTOR: BaseDbContext renamed to FrameworkDbContext

**v0.1.3.0 (2017-02-14)**
* RENAMED: IRepository.ListAll renamed to GetAll
* NEW: IRepository.GetBy
* NEW: BaseDbContext.CreateSessionScope
* REMOVED: FrameworkController.ExecuteAjax
* NEW: FrameworkController.ExecuteJsonResult

**v0.1.2.0 (2017-02-10)**
* REMOVED: IDbEngine.ExecuteCommand
* REMOVED: TextReplacer
* NEW: DbSriptExecuteOptions
* NEW: CommandLine
* FrameworkDatabase use lazy creation of sessionFactory

**v0.1.1.1 (2017-02-09)**
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute

**v0.1.1.0 (2017-02-09)**
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute

**v0.1.0.15 (2017-01-22)**
* FIXED: IQuery<T>.Set unified

**v0.1.0.14 (2017-01-22)**
* FIXED: Pager result set Disabled and Active
* FIXED: Pager always set text to number
* REFACTOR: Query and Query<T> return self
* NEW: QueryDTO.ToCritias

**v0.1.0.13 (2017-01-22)**
* NEW: Add ContentReplacer/WorkingDirectory parameter to FrameworkDatabase

**v0.1.0.12 (2017-01-21)**
* NEW: CritiaList
* REMOVED: RedirectData and FrameworkController.ExecuteAndRedirect(RedirectData)

**v0.1.0.11 (2017-01-19)**
* FIX: ExecuteAndRedirect set TempMessage
* NEW: ExecuteAndRedirect successMessage can be null
* NEW: ExecuteAndRedirect redirect param set to url
* NEW: MemberUtil

