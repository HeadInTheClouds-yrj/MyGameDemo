# 所有UI使用到的组件
content size fitter（把这个组件添加在ScrollViewUI->Viewport->Content上能流畅的控制content下的子对象的滑动）;
其他创建某种ui时自动添加的组件
# SaveLoad系统主要使用到的脚本[参考](https://github.com/shapedbyrainstudios/save-load-system"保存系统")
#### JsonUtility: unity引擎自带序列化逆序列化工具.

#### ISerializationCallbackReceiver:继承这个class是因为JsonUtility它序列化不了复杂的数据，比如dictionary<TKey,TValue>,用法是创建一个新的C#脚本，让这个脚本继承 Dictionary<TKey,TValue>和 ISerializationCallbackReceiver.

#### ISerializationCallbackReceiver里的OnBeforeSerialize()这个方法是在序列化成json文件之前运行的（也就是保存为json文件之前），所以需要把其他使用了这个泛型脚本存dictionary数据的类的key和value分别存在List<Key>和List<Value>里，这样就能把dictionary的数据序列化了.

#### ISerializationCallbackReceiver里的OnAfterDeserialize()这个方法是在逆序列化之后运行的（load之后），逆序列化之后List<Key> & List<Value> 这俩集合里面有之前分别存的dictionary数据，把List<Key> & List<Value>遍历存进this就能还原dictionary数据.

# QuestSystem任务系统主要思路[参考](https://github.com/shapedbyrainstudios/quest-system"任务系统")
## 思路：
#### 首先考虑能不能保存，再一个是先后顺序和多结局，然后是任务是否易于编写和管理。
## 核心架构：
#### manager：负责初始化所有的任务，检查并改变任务的状态
#### Quest：保存着任务的所有当前状态
#### QuestEvent：负责更新任务状态，可以在EventManager单例脚本拿到并调用
#### QuestPoint：任务接取的入口，负责接收任务
#### QuestStep：负责与具体任务步骤状态的传递和更新到quest脚本
# Editor Version
2022.2.0b16
# 美术资源说明
此项目不会用商用,美术资源为其他美术作者的作品,除了特效.
