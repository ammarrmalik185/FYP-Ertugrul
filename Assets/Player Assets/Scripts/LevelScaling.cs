namespace Player_Assets.Scripts{
    public static class LevelScaling{
        public static float getTotalXp(int level){
            return level switch{
                0 => 0,
                1 => 100,
                2 => 120,
                3 => 150,
                4 => 200,
                5 => 250,
                6 => 310,
                7 => 400,
                8 => 500,
                9 => 700,
                _ => 1000
            };
        }
        public static int getTotalHp(int level){
            return level switch{
                0 => 0,
                1 => 100,
                2 => 120,
                3 => 150,
                4 => 200,
                5 => 250,
                6 => 310,
                7 => 400,
                8 => 500,
                9 => 700,
                _ => 1000
            };
        }
    }
}