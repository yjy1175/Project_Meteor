# Data 
## Data 구조
* 운석부수기는 서버를 이용하는 게임이 아니기에 게임내 데이터는 json파일로 관리를 해주고 있습니다.
* 기획자와 Data driven을 통한 개발을 하기위해 일정한 룰을 정하여 json만을 수정하더라도 게임의 데이터를 변경할 수 있도록 설계하였습니다.
* [json폴더](../../Assets/Resources/GameData)에서 운석부수기에서 활용한 json파일들을 확인하실 수 있습니다.

## Data Load 구조
* [DataManager.cs](../../Assets/Scripts/Data/DataManager.cs)에서 각 게임에 필요한 정적인 데이터들을 DataHolder 클래스를 통하여 들고있습니다.
게임 시작 전에 로컬에 있는 json파일을 파싱하여 [DataObject.cs](../../Assets/Scripts/Data/DataObject.cs)에 선언되어있는 구조체에 따라 데이터를 삽입해줍니다.
* [DataHolder.cs](../../Assets/Scripts/Data/DataHolder.cs)는 Generic클래스이며 다양한 데이터 타입을 지정할 수 있도록 설계하였습니다.

## 유저 저장 데이터 Save 및 Load
* 유저의 로컬 저장 데이터도 마찬가지로 DataManager에서 관리해줍니다.
* DataManager의 SaveLocalData()와 UpdatePalyerSaveData()를 통하여 적재적소에서 Save 및 Load를 할 수 있게 설계하였습니다.