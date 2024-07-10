# 所有UI使用到的组件
content size fitter（把这个组件添加在ScrollViewUI->Viewport->Content上能流畅的控制content下的子对象的滑动）;

IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler //拖拽接口，实现之后会有几个事件方法能用，方法的生命周期就是方法名字字面意思。

其他创建某种ui时自动添加的组件....

# SaveLoad系统主要使用到的脚本[参考](https://github.com/shapedbyrainstudios/save-load-system)
#### JsonUtility: unity引擎自带序列化逆序列化工具.

#### ISerializationCallbackReceiver:继承这个class是因为JsonUtility它序列化不了复杂的数据，比如dictionary<TKey,TValue>,用法是创建一个新的C#脚本，让这个脚本继承 Dictionary<TKey,TValue>和 ISerializationCallbackReceiver.

#### ISerializationCallbackReceiver里的OnBeforeSerialize()这个方法是在序列化成json文件之前运行的（也就是保存为json文件之前），所以需要把其他使用了这个泛型脚本存dictionary数据的类的key和value分别存在List<Key>和List<Value>里，这样就能把dictionary的数据序列化了.

#### ISerializationCallbackReceiver里的OnAfterDeserialize()这个方法是在逆序列化之后运行的（load之后），逆序列化之后List<Key> & List<Value> 这俩集合里面有之前分别存的dictionary数据，把List<Key> & List<Value>遍历存进this就能还原dictionary数据.

#### 收集数据主要靠Scene Manager的OnSceneLoaded（）事件进行对继承了特定抽象类的脚本的收集.

# QuestSystem任务系统主要思路[参考](https://github.com/shapedbyrainstudios/quest-system)
## 思路：
#### 首先考虑能不能保存，再一个是先后顺序和多结局，然后是任务是否易于编写和管理。
## 核心架构：
#### QuestManager：负责初始化所有的任务，检查并改变任务的状态.
#### Quest：保存着任务的所有当前状态.
#### QuestEvent：负责更新任务状态，可以在EventManager单例脚本拿到并调用.
#### QuestPoint：任务接取的入口，负责接收任务.
#### QuestStep：负责与具体任务步骤状态的传递和更新到quest脚本.

# 有限状态机FSM Animation控制

#### 这个项目没用到，之前做的demo有用到.
#### FSMBase：可更改的生命周期方法，不继承monobehaviour.
#### FSMManger：实现对FSMBase集合状态的添加，改变，开始，循环，退出功能.
#### AnimationControl：继承FSMBase的各个动画类.
#### 具体使用：实例化FSMManger，使用各种控制方法，传入需要更改的参数状态.

# AI寻路
#### EnemyAI ：负责执行移动和攻击逻辑方法。
#### AIData：顾名思义保存行为意向数据的，包括检测到的目标，障碍物，计算出的移动方向和不可移动的方向权重。

#### Detector：检测抽象类 .
#### ObstacleDetector：继承Detector，检测周围的障碍物，保存到AIData.
#### TargetDetector：继承Detector，检测周围的目标，保存到AIData.

#### SteeringBehaviour： 收集转向方向权重数据抽象类.
#### SeekBehaviour：继承SteeringBehaviour，计算八个方向目标权重，基于AIData数据里的targets，保存八个方向的权重数据到AIData.
#### ObstacleAvoidanceBehaviour：继承SteeringBehaviour，计算八个方向障碍权重，基于AIData数据里的obstacles，保存八个方向的权重数据到AIData.

#### ContextSolver：解析得到的数据，得出最佳移动方向（目标权重 - 障碍权重： 最后比大小）.
# Unity Engine Version
2022.2.0b16
# 美术资源说明
此项目不会用商用,美术资源为其他美术作者的作品,除了特效.
