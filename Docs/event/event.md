## Event
# delegate와 event를 활용한 Observing Pattern
* 매 update마다 특정 GameObject의 값을 체크하는게 아닌 변화가 생겼을 때 정보를 얻고싶은 값에 대해 Event를 등록하여 값이 변화할때마다 등록된 EventHandler들에 대해 Invoke하여 변화를 알려주는 방식입니다.
* [EventManager.cs](../../Assets/Scripts/Event/EventManager.cs)를 통하여 미리 사용할 delegate를 각 타입에 맞게 선언하여 사용하였습니다.
* [PlayerStats.cs](../../Assets/Scripts/Player/PlayerStats.cs)에서 보시다 시피 사용할 클래스에서 event를 선언하여 값의 변화에 대응하도록 사용하였습니다.