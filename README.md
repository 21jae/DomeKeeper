# Unity Game Portfolio: [DomeKeeper] 
![Snipaste_2023-12-13_23-18-52](https://github.com/21jae/DomeKeeper/assets/90013449/32891ce8-d2d4-4a34-bef4-1c2d85f51705) </br>

## 개요
제목 | DomeKeeper 모작 
------------ | ------------- 
장르 | 생존형 디펜스 게임
개발 기간 | 3주
사용 툴 | Unity
팀원 | 4명
기획 의도 | 생존형과 디펜스게임의 단점인 지루함을 없애고자 긴장감있는 생존형 디펜스를 채택하여 팀원과 함께 모작하였습니다.</br>

## 게임소개
![Domeee](https://github.com/21jae/DomeKeeper/assets/90013449/5a608277-ad74-4a4c-b306-8306d1cb4f44) </br>
Dome 안에서 웨이브마다 몰려오는 적들을 막아내면 승리하는 게임입니다. 이를 위해 Player는 지하에서 광물을 채광하며 얻은 광물로
Dome 을 업그레이드 해야만하며, 방어와 채광을 동시에 진행해야하므로 시간 배분을 잘 해야하는 게임입니다.
</br>



## 담당한 기능
* 주변 광물 감지 및 채광
  * 펫은 Dome에서 출발할때마다 새로운 목적지를 얻고, 그 위치로 최단거리로 나아갑니다.
  * 이동중에 광물이 레이어에 걸리면 채광합니다. </br>
  ![ezgif-5-a172fb46db](https://github.com/21jae/DomeKeeper/assets/90013449/c053abf6-e661-441d-8a30-362b57695f23) </br>
  
* 최단거리 길찾기 및 복귀 </br>
  * 광물 보유수가 최대치라면, Dome을 최단거리 루트로 돌아옵니다. </br>
  ![ezgif-5-3b1f56fc67](https://github.com/21jae/DomeKeeper/assets/90013449/f134b0de-4742-454b-849e-094c3ea848a7) </br>
  ![Snipaste_2023-12-13_22-53-34](https://github.com/21jae/DomeKeeper/assets/90013449/878f2562-46b8-4e5e-b520-5f95e02b588f) </br>
* Player 상태머신(FSM)
* SkillManager
</br>

## 배운점
* 상태머신을 사용하지않고 Pet 스크립트를 작성하였는데, 무한 if문이 반복되어 알아보기 힘들어졌습니다. 이를통해 스크립트를 구현할때는 어떤 방법을 선택해야 내가 편하고 팀원이 편한지 생각하게 되었습니다.
* 이 프로젝트를 진행할때는 Github뿐 아니라 [Trello](https://trello.com/b/QUnailvm/domekeeper)도 같이 사용하여 협업을 진행하였고, 이러한 프로그램을 적절히 쓴다면 더욱 효율이 올라간다는 것을 깨달았습니다.
* 길찾기 원리를 공부하였습니다.
</br>

## 플레이 영상
영상 : https://www.youtube.com/watch?v=RMpdOEEkrX0





