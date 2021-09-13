namespace NiceShot.Core.Models.Entities
{
    public class measuring_the_moat
    {
        public string ts_code { get; set; }

        //概况
        public string competitive_life_cycle_stage { get; set; }
        public bool? is_return_above_cost { get; set; }
        public string roic_status { get; set; }
        public string roic_status_remarks { get; set; }
        public string invest_spending_trend { get; set; }

        //了解情况
        public string each_player_represent_percent { get; set; }
        public string each_player_profit_level { get; set; }
        public string market_share_historical_trends { get; set; }
        public string market_share_how_stable { get; set; }
        public string pricing_trends { get; set; }
        public string industry_class { get; set; }

        //五力模型的前三个
        public string suppliers_leverage { get; set; }
        public bool? pass_supplier_incto_customers { get; set; }
        public bool? substi_prod_available { get; set; }
        public bool? have_switching_costs { get; set; }
        public string buyers_leverage { get; set; }
        public string buyers_informed { get; set; }

        //进入的障碍
        public string entry_and_exit_rates { get; set; }
        public string reactions_newentrants { get; set; }
        public string reputation { get; set; }
        public string asset_specificity { get; set; }
        public string mini_effic_prod_scale { get; set; }
        public bool? excess_capacity { get; set; }
        public bool? way_to_diff_prod { get; set; }
        public string antici_payoff_for_new_entrant { get; set; }
        public bool? precommit_contracts { get; set; }
        public bool? licenses_or_patents { get; set; }
        public string learning_curve_benefits { get; set; }

        //竞争对手
        public string pricing_coordination { get; set; }
        public string industry_concentration { get; set; }
        public string size_distribution { get; set; }
        public string incent_corp_ownership_structure { get; set; }
        public bool? demand_variability { get; set; }
        public bool? high_fixed_costs { get; set; }
        public bool? industry_growing { get; set; }

        //破坏和瓦解
        public bool? vulnerable_disruptive_innovation { get; set; }
        public bool? innovations_foster_prod_improve { get; set; }
        public bool? inno_progre_faster_market_needs { get; set; }
        public bool? estab_players_passed_perform_threshold { get; set; }
        public string organized_type { get; set; }

        //公司相关
        public string value_chain_diff_rivals { get; set; }
        public bool? production_advantages { get; set; }
        public bool? biz_stuct_instable { get; set; }
        public bool? complex_co_cap { get; set; }
        public string how_quick_process_costs_chg { get; set; }
        public string patents_copyrights { get; set; }
        public bool? economies_scale { get; set; }
        public string distribution_scale { get; set; }
        public bool? ass_reve_clustered_geo { get; set; }
        public bool? purchasing_advantages { get; set; }
        public bool? economies_scope { get; set; }
        public bool? diverse_research_profiles { get; set; }
        public bool? consumer_advantages { get; set; }
        public bool? habit_horizontal_diff { get; set; }
        public string prefer_pro_or_competing_pro { get; set; }
        public bool? customers_weigh_lots_pro_attrs { get; set; }
        public bool? customers_through_trial { get; set; }
        public string customer_lockin_switching_costs { get; set; }
        public string network_radial_or_interactive { get; set; }
        public string source_longevity_added_value { get; set; }
        public string external_sources_added_value { get; set; }

        //企业互动-竞争与协调
        public string industry_include_complementors { get; set; }
        public string value_pie_growing { get; set; }

        //品牌
        public bool? hire_brand_for_job { get; set; }
        public bool? brand_increase_will_to_pay { get; set; }
        public bool? emotional_conn_brand { get; set; }
        public bool? trust_pro_cause_name { get; set; }
        public bool? brand_imply_social_status { get; set; }
        public bool? reduce_supplier_opercost_with_name { get; set; }

    }
}
