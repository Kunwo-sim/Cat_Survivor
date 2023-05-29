# HollyCat  
![image](https://user-images.githubusercontent.com/77709696/232675217-692855cf-3c99-46c7-a541-bd4b4cbb2662.png)  
브릿지 프로젝트 HollyCat  
대학생게임제작연합동아리 브릿지(Bridge)에서 2022-2학기에 진행한 프로젝트입니다.  

## 게임소개  
HollyCat은 십이지신 소재로 한 로그라이크 액션 게임입니다.  
플레이어는 십이지신에 포함되지 못한 고양이가 되어 고양이의 십이지신 입성을 저지하려는 다른 동물들과 결투를 하게 됩니다.  
![image](https://user-images.githubusercontent.com/77709696/232675242-92f12851-e634-4e7a-bff8-1e84f36e5edd.png)  
레벨업을 할 때 마다 다양한 스탯을 증가시켜 더욱 강력한 고양이로 성장할 수 있습니다.  
![image](https://user-images.githubusercontent.com/77709696/232675332-c74665fe-1592-460e-8a12-6e043258d3b6.png)  
동물들을 처치하고 모은 코인으로 더 화려한 무기를 구매하거나, 아이템을 구매하여 스탯을 올릴 수 있습니다.  
![image](https://user-images.githubusercontent.com/77709696/232675307-340c63d1-4883-4a2d-9de6-efc3abe0e712.png)  
  
## 코드소개  
![image](https://user-images.githubusercontent.com/77709696/232678292-82259bc8-27a1-471b-9421-ca3508bbbae3.png)  
CSV 파일에서 아이템 정보와 능력치 정보를 가져옵니다.  
![image](https://user-images.githubusercontent.com/77709696/232678192-a8cd1388-34d1-484f-abf1-e56ad8238e24.png)  
가져온 정보를 Dictionary로 저장하여 필요시 key 값으로 가져올 수 있게 하였습니다.  
![image](https://user-images.githubusercontent.com/77709696/232678779-157bbf47-8646-434b-b840-66af6398a9d0.png)  
로딩 화면을 구현해 검은 화면에서 바로 인게임 화면으로 넘어갈 수 있게 구현하였습니다.
![image](https://user-images.githubusercontent.com/77709696/232680158-eefe005c-3d8c-4ff5-8b8d-ae059cfe0233.png)  
DOTween을 활용해 로고 애니메이션 효과를 주었습니다.
![image](https://user-images.githubusercontent.com/77709696/232681308-159682d8-36c2-43eb-90c3-2807b336235f.png)  
RTTI를 활용하여 enum과 Gameobject를 연동하였습니다. 하이어라키 창에서 드래그앤드롭을 하지 않고도 코드상에서 원하는 GameObject를 불러올 수 있습니다.
