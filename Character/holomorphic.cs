package{
  import flash.display.MovieClip;
  import flash.events.Event;
  import flash.events.MouseEvent;
  import flash.display.*;
  import flash.events.*;
  import flash.net.*;
  import flash.geom.Rectangle;
  import flash.media.Sound;
  import flash.text.*;
  public class Game extends MovieClip{
  public static const STATE_INIT: int =10;
  public static const STATE_PLAY: int =20;
  public static const STATE_END_GAME: int =30;
  public var gameState:int =0;
  public var score:int=0;
  public var level:Number =0;
  public var chance:int =0;
  public var bg:MovieClip;
  public var enemies:Array;
  public var player:MovieClip;
  public var scoreLabel:TextField = new TextField();
  public var levelLabel:TextField = new TextField();
  public var chanceLabel:TextField = new TextField();
  public var scoreText:TextField = new TextField();
  public var levelText:TextField = new TextField();
  public var chanceText:TextField = new TextField();
  public const SCOREBOARD_Y:Number=380;
  public function Game(){
  addEventListener(Event.ENTER_FRAME,gameLoop);
  bg=new BackImage();
  addChild(bg);
  scoreLabel.text="Score:";
  levelLabel.text="level:";
  chanceLabel.text="Misses:";
  scoreText.text="0";
  levelText.text="1";
  chanceText.text="0";
 
  scoreLabel.y=SCOREBOARD_Y;
  levelLabel.y=SCOREBOARD_Y;
  chanceLabel.y=SCOREBOARD_Y;
  scoreText.y=SCOREBOARD_Y;
  levelText.y=SCOREBOARD_Y;
  chanceText.y=SCOREBOARD_Y;
 
  scoreLabel.x=5;
  scoreText.x=50;
  chanceLabel.x=105;
  chanceText.x=155;
  levelLabel.x=205;
  levelText.x=260;
 
  addChild(scoreLabel);
  addChild(scoreText);
  addChild(levelLabel);
  addChild(levelText);
  addChild(chanceLabel);
  addChild(chanceText);
 
  gameState=STATE_INIT;
}
public function gameLoop(e:Event):void{
switch(gameState){
case STATE_INIT:
initGame();
break;
case STATE_PLAY:
playGame();
break;
case STATE_END_GAME:
endGame();
break;
}
}
public function initGame():void{
score=0;
chance=0;
level=1;
levelText.text=level.toString();
player=new PlayerImage();
addChild(player);
player.startDrag(true,new Rectangle(0,0,550,400));
enemies=new Array();
gameState=STATE_PLAY;
}
public function playGame():void{
player.rotation+=15;
makeEnemies();
moveEnemies();
testCollisions();
testForEnd();
}
public function makeEnemies():void{
var chance:Number =Math.floor(Math.random()*100);
if(chance<2+level){
var tempEnemy:MovieClip;
tempEnemy=new EnemyImage();
tempEnemy.speed=3+level;
tempEnemy.gotoAndStop(Math.floor(Math.random()*5)+1);
tempEnemy.y=435;
tempEnemy.x=Math.floor(Math.random()*515);
addChild(tempEnemy);
enemies.push(tempEnemy);
}
}
public function moveEnemies():void{
var tempEnemy:MovieClip;
for(var i: int = enemies.length-1;i>=0;i--){
tempEnemy=enemies[i];
tempEnemy.y-=tempEnemy.speed;
if(tempEnemy.y<-35){
chance++;
chanceText.text=chance.toString();
enemies.splice(i,1);
removeChild(tempEnemy);
}
}
}
public function testCollisions():void{
var tempEnemy:MovieClip;
var sound:Sound=new Pop();
for(var i:int =enemies.length-1;i>=0;i--){
tempEnemy=enemies[i];
if(tempEnemy.hitTestObject(player)){
score++;
scoreText.text=score.toString();
sound.play();
enemies.splice(i,1);
removeChild(tempEnemy);
}
}
}
public function testForEnd(){
if(chance>=5){
gameState=STATE_END_GAME;
}
else{
if(score>=level*20){
level++;
levelText.text=level.toString();
}
}
}
public function endGame(){
for(var i:int =enemies.length-1;i>=0;i--){
removeChild(enemies[i]);
}
enemies=[];
player.stopDrag();
}

  }
  }