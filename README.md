# Graduation Project
[사용 툴 : Unity 3D]
[제작 기간 : 2020.09 – 2021.05]
[제작인원 : 1 명]
[장르 : FPS]

✓ Character : 적과 플레이어의 기본 스탯을 ScriptTableObject 로 구성하였고 캐릭터들의 데미지를 받고 HP 감소 및 증가에 대해 겹쳐지는 부분과 죽었을 때 서로 작용하는 부분이 달라 virtual, override 를 통해 오브젝트들의 상속 코드 작성
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/tree/master/Graduation%20Project/Assets/Script/Character%20Base

✓ Weapon : 근접 공격, 원거리 공격 모두 기본적인 것을 부모 자식 간의 설정으로 코드 재 사용성을 높였으며 근접, 원거리 모두 RayCast 방식을 사용
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/tree/master/Graduation%20Project/Assets/Script/Weapon

✓ Minimap : Player 의 위치를 기준으로 Enemy 의 위치를 계산하고 그에 따른 미니맵 Ui 이미지를 배치하여 미니맵 상에 보여지게 코드 작성
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/blob/master/Graduation%20Project/Assets/Script/Ui/MiniMap.cs

✓ Stage Controller : Stage 마다 주로 하는 목표와 목표물 등이 달라지기에 기초적으로 공통되는 부분을 부모 클래스에 넣어두고 상속을 하여 스테이지 별 달라지는 것들을 override 로 구성하여 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/tree/master/Graduation%20Project/Assets/Script/Controller

✓ Data Controller : Json 을 사용하여 Data 저장 및 로드 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/tree/master/Graduation%20Project/Assets/Script/Controller

✓ Enemy : Behavior Tree 에디터를 사용하여 기본적으로 주어지는 노드들을 이용하고 필요한 기능의 노드를 코딩을 하여 적의 종류마다 다른 패턴을 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/tree/master/Graduation%20Project/Assets/Script/Enemy

✓ Barrel Explosion : Barrel 에 충격을 가하면 폭발과 동시에 주변 능동 오브젝트 뒤로 물러나게 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/blob/master/Graduation%20Project/Assets/Script/Weapon/Barrel.cs
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/blob/master/Graduation%20Project/Assets/Script/Character%20Base/CharacterBase.cs

✓ 마우스 민감도 : 특정 키를 통해 카메라 속도 조절하여 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/blob/master/Graduation%20Project/Assets/Script/MainCamera.cs

✓ Boss : Behavior Tree 에디터를 이용하여 점프 공격, 돌진 공격, 산성 침 뱉기 총 3 가지 패턴을 구현하였고 각 패턴마다 콜라이더를 따로 둬서 패턴이 시작했을 때 SetActive 를 True 로 끝났을 때는 False 로 바꿔가며 플레이어에게 데미지를 줄 수 있도록 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/blob/master/Graduation%20Project/Assets/Script/Enemy/BossEnemy.cs

✓ Game Stop : deligate 를 사용하여 게임을 멈추고 다시 시작할 때의 필요한 것을 담아서 한번 호출로 여러 개의 함수를 호출 할 수 있도록 구현
* 코드 위치 : https://github.com/ChoGwangHyung/Graduation-Project/blob/master/Graduation%20Project/Assets/Script/Controller/GameController.cs

https://www.youtube.com/playlist?list=PLKXf0qhZKKaewCYZaOvtjjkJeuhTCj6Qq
