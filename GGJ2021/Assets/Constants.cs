using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Constants
    {
        public static string animator_trigger_foundOwner = "FoundOwner";
        public static string animator_trigger_arrested = "Arrested";
        public static string animator_float_mustacheDistanceToOwner = "mustacheDistanceToOwner";
        public static string animator_bool_isWalking = "isWalking";
        public static string animator_bool_npc_isWalking = "isWalking";
        public static string animator_bool_player_wet = "isWet";
        public static string animator_bool_owner_happy = "isHappy";

        public static string tag_HudToShowAfterFade = "HUD";
        public static string tag_FadeCanvas = "Fade";

        //BB constants
        public static float min_dist_bb_to_control_point = 0.5f;
        public static float bb_collision_circle_radius = 4;
        public static float min_dist_bb_to_catch = 2.0f;
        public static int bb_stopped_colliding_interval_sec = 2;
        public static int bb_stopped_following_interval_sec = 2;

        //Player constants
        public static float interval_to_enter_house_again_sec = 3.0f;
        public static float interval_is_wet = 5.0f;

        //Owner constants
        public static float interval_owner_happy = 3.0f;

        //Tags
        public static string player_tag = "Player";
        public static string inside_building_tag = "InsideBuilding";
        public static string animal_tag = "Animal";
        public static string building_tag = "Building";
        public static string owner_tag = "Owner";
        public static string hud_tag = "HUD";

    }
}
