//首先创建一个Person类 其中包含私有属性(private)：姓名（name）、身高（height）、武器（weapon）、种族（race）。
public class Person {
    private String name;
    private double height;
    private String weapon;
    private String race;
    
    //Alt+Insert 选中Select None创建Person类的无参构造方法
    public Person() {
    }
    
	//Alt+Insert 全部选中(Alt+A)点击OK，构造方法直接给成员变量赋值
    public Person(String name, double height, String weapon, String race) {
        this.name = name;
        this.height = height;
        this.weapon = weapon;
        this.race = race;
    }
    
    //Ctrl+o 方法重写 选中Object下的toString方法，进行重写
    @Override
    public String toString() {
        return "姓名:"+name+"\t种族:"+race;
    }
    
	//创建skill方法。name.equals("孙悟空") ==>  利用equals判断name的属性值是否跟"孙悟空"相等，如果相等则输出"技能:七十二变"，以下同理
    public void skill() {
        if (name.equals("孙悟空")) {
            System.out.println("技能:七十二变");
        }else if (name.equals("唐僧")) {
            System.out.println("技能:紧箍咒");
        }else if (name.equals("白骨精")) {
            System.out.println("技能:九阴白骨爪");
        }else {
            System.out.println("没有这个人物");
        }
    }
	
    //重写equals方法 obj默认为Object类 将obj强转为P
    @Override
    public boolean equals(Object obj) {
            Person person = (Person) obj;
            if ("妖族".equals(this.race) || "妖族".equals(person.race)) {
                return false;
            }
        return true;
    }
    
    
	//Alt+Insert 选择get and set方法 z
    public String getName() {
        return name;
    }
 
    public void setName(String name) {
        this.name = name;
    }
 
    public double getHeight() {
        return height;
    }
 
    public void setHeight(double height) {
        this.height = height;
    }
 
    public String getWeapon() {
        return weapon;
    }
 
    public void setWeapon(String weapon) {
        this.weapon = weapon;
    }
 
    public String getRace() {
        return race;
    }
 
    public void setRace(String race) {
        this.race = race;
    }
}
public static void main(String[] args) {
    	//实例化对象
        Person tangSeng = new Person("唐僧", 178, "权杖", "人族");
        Person sunWuKong = new Person("孙悟空", 185, "金箍棒", "人族");
    	//调用toString方法输出孙悟空姓名，种族
        System.out.println(sunWuKong.toString());
    	//调用skill方法输出孙悟空技能
        sunWuKong.skill();
        Person baiGuJing = new Person("白骨精", 177, "骨头", "妖族");
    	//调用equals方法进行判断，如果种族属性是人族或者仙族，则返回true，如果是妖族，则返回false
        System.out.println(tangSeng.getRace().equals(sunWuKong.getRace()));
        System.out.println(tangSeng.getRace().equals(baiGuJing.getRace()));
    }