/*
 Navicat Premium Data Transfer

 Source Server         : kimdb
 Source Server Type    : MySQL
 Source Server Version : 50731
 Source Host           : localhost:3306
 Source Schema         : niceshotdb

 Target Server Type    : MySQL
 Target Server Version : 50731
 File Encoding         : 65001

 Date: 13/09/2021 08:13:53
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for att_rd
-- ----------------------------
DROP TABLE IF EXISTS `att_rd`;
CREATE TABLE `att_rd`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `enddate` date NULL DEFAULT NULL,
  `RDPerson` int(11) NULL DEFAULT NULL,
  `RDPersonRatio` decimal(18, 2) NULL DEFAULT NULL,
  `RDSpendSum` decimal(18, 2) NULL DEFAULT NULL,
  `RDSpendSumRatio` decimal(18, 2) NULL DEFAULT NULL,
  `RDInvest` decimal(18, 2) NULL DEFAULT NULL,
  `RDInvestRatio` decimal(18, 2) NULL DEFAULT NULL,
  `Explanation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_rd_tscode`(`ts_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2542 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bonus2021
-- ----------------------------
DROP TABLE IF EXISTS `bonus2021`;
CREATE TABLE `bonus2021`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `bonus` decimal(18, 2) NULL DEFAULT NULL,
  `prepaid_accounts` decimal(18, 2) NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_a_mainindex
-- ----------------------------
DROP TABLE IF EXISTS `em_a_mainindex`;
CREATE TABLE `em_a_mainindex`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `yyzsr` decimal(18, 2) NULL DEFAULT NULL,
  `gsjlr` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsrtbzz` decimal(18, 2) NULL DEFAULT NULL,
  `gsjlrtbzz` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlrtbzz` decimal(18, 2) NULL DEFAULT NULL,
  `jqjzcsyl` decimal(18, 2) NULL DEFAULT NULL,
  `tbjzcsyl` decimal(18, 2) NULL DEFAULT NULL,
  `tbzzcsyl` decimal(18, 2) NULL DEFAULT NULL,
  `mll` decimal(18, 2) NULL DEFAULT NULL,
  `jll` decimal(18, 2) NULL DEFAULT NULL,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `date` date NULL DEFAULT NULL,
  `markettype` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `bonus` decimal(18, 2) NULL DEFAULT NULL,
  `sales` decimal(18, 2) NULL DEFAULT NULL,
  `lxzbh` decimal(18, 2) NULL DEFAULT NULL,
  `total_jkfy` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `date`(`date`) USING BTREE,
  INDEX `jqjzcsyl`(`jqjzcsyl`) USING BTREE,
  INDEX `kfjlr`(`kfjlr`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 221690 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_bal_common_v1
-- ----------------------------
DROP TABLE IF EXISTS `em_bal_common_v1`;
CREATE TABLE `em_bal_common_v1`  (
  `id` bigint(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `secucode` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `security_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `report_date` date NULL DEFAULT NULL,
  `notice_date` date NULL DEFAULT NULL,
  `update_date` date NULL DEFAULT NULL,
  `accept_deposit_interbank` decimal(18, 2) NULL DEFAULT NULL,
  `accounts_payable` decimal(18, 2) NULL DEFAULT NULL,
  `accounts_rece` decimal(18, 2) NULL DEFAULT NULL,
  `accrued_expense` decimal(18, 2) NULL DEFAULT NULL,
  `advance_receivables` decimal(18, 2) NULL DEFAULT NULL,
  `agent_trade_security` decimal(18, 2) NULL DEFAULT NULL,
  `agent_underwrite_security` decimal(18, 2) NULL DEFAULT NULL,
  `amortize_cost_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `amortize_cost_finliab` decimal(18, 2) NULL DEFAULT NULL,
  `amortize_cost_ncfinasset` decimal(18, 2) NULL DEFAULT NULL,
  `amortize_cost_ncfinliab` decimal(18, 2) NULL DEFAULT NULL,
  `appoint_fvtpl_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `appoint_fvtpl_finliab` decimal(18, 2) NULL DEFAULT NULL,
  `asset_balance` decimal(18, 2) NULL DEFAULT NULL,
  `asset_other` decimal(18, 2) NULL DEFAULT NULL,
  `assign_cash_dividend` decimal(18, 2) NULL DEFAULT NULL,
  `available_sale_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `bond_payable` decimal(18, 2) NULL DEFAULT NULL,
  `borrow_fund` decimal(18, 2) NULL DEFAULT NULL,
  `buy_resale_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `capital_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `cip` decimal(18, 2) NULL DEFAULT NULL,
  `consumptive_biological_asset` decimal(18, 2) NULL DEFAULT NULL,
  `contract_asset` decimal(18, 2) NULL DEFAULT NULL,
  `contract_liab` decimal(18, 2) NULL DEFAULT NULL,
  `convert_diff` decimal(18, 2) NULL DEFAULT NULL,
  `creditor_invest` decimal(18, 2) NULL DEFAULT NULL,
  `current_asset_balance` decimal(18, 2) NULL DEFAULT NULL,
  `current_asset_other` decimal(18, 2) NULL DEFAULT NULL,
  `current_liab_balance` decimal(18, 2) NULL DEFAULT NULL,
  `current_liab_other` decimal(18, 2) NULL DEFAULT NULL,
  `defer_income` decimal(18, 2) NULL DEFAULT NULL,
  `defer_income_1year` decimal(18, 2) NULL DEFAULT NULL,
  `defer_tax_asset` decimal(18, 2) NULL DEFAULT NULL,
  `defer_tax_liab` decimal(18, 2) NULL DEFAULT NULL,
  `derive_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `derive_finliab` decimal(18, 2) NULL DEFAULT NULL,
  `develop_expense` decimal(18, 2) NULL DEFAULT NULL,
  `div_holdsale_asset` decimal(18, 2) NULL DEFAULT NULL,
  `div_holdsale_liab` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_payable` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_rece` decimal(18, 2) NULL DEFAULT NULL,
  `equity_balance` decimal(18, 2) NULL DEFAULT NULL,
  `equity_other` decimal(18, 2) NULL DEFAULT NULL,
  `export_refund_rece` decimal(18, 2) NULL DEFAULT NULL,
  `fee_commission_payable` decimal(18, 2) NULL DEFAULT NULL,
  `fin_fund` decimal(18, 2) NULL DEFAULT NULL,
  `finance_rece` decimal(18, 2) NULL DEFAULT NULL,
  `fixed_asset` decimal(18, 2) NULL DEFAULT NULL,
  `fixed_asset_disposal` decimal(18, 2) NULL DEFAULT NULL,
  `fvtoci_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `fvtoci_ncfinasset` decimal(18, 2) NULL DEFAULT NULL,
  `fvtpl_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `fvtpl_finliab` decimal(18, 2) NULL DEFAULT NULL,
  `general_risk_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `goodwill` decimal(18, 2) NULL DEFAULT NULL,
  `hold_maturity_invest` decimal(18, 2) NULL DEFAULT NULL,
  `holdsale_asset` decimal(18, 2) NULL DEFAULT NULL,
  `holdsale_liab` decimal(18, 2) NULL DEFAULT NULL,
  `insurance_contract_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `intangible_asset` decimal(18, 2) NULL DEFAULT NULL,
  `interest_payable` decimal(18, 2) NULL DEFAULT NULL,
  `interest_rece` decimal(18, 2) NULL DEFAULT NULL,
  `internal_payable` decimal(18, 2) NULL DEFAULT NULL,
  `internal_rece` decimal(18, 2) NULL DEFAULT NULL,
  `inventory` decimal(18, 2) NULL DEFAULT NULL,
  `invest_realestate` decimal(18, 2) NULL DEFAULT NULL,
  `lease_liab` decimal(18, 2) NULL DEFAULT NULL,
  `lend_fund` decimal(18, 2) NULL DEFAULT NULL,
  `liab_balance` decimal(18, 2) NULL DEFAULT NULL,
  `liab_equity_balance` decimal(18, 2) NULL DEFAULT NULL,
  `liab_equity_other` decimal(18, 2) NULL DEFAULT NULL,
  `liab_other` decimal(18, 2) NULL DEFAULT NULL,
  `loan_advance` decimal(18, 2) NULL DEFAULT NULL,
  `loan_pbc` decimal(18, 2) NULL DEFAULT NULL,
  `long_equity_invest` decimal(18, 2) NULL DEFAULT NULL,
  `long_loan` decimal(18, 2) NULL DEFAULT NULL,
  `long_payable` decimal(18, 2) NULL DEFAULT NULL,
  `long_prepaid_expense` decimal(18, 2) NULL DEFAULT NULL,
  `long_rece` decimal(18, 2) NULL DEFAULT NULL,
  `long_staffsalary_payable` decimal(18, 2) NULL DEFAULT NULL,
  `minority_equity` decimal(18, 2) NULL DEFAULT NULL,
  `monetaryfunds` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_asset_1year` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_asset_balance` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_asset_other` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_liab_1year` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_liab_balance` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_liab_other` decimal(18, 2) NULL DEFAULT NULL,
  `note_accounts_payable` decimal(18, 2) NULL DEFAULT NULL,
  `note_accounts_rece` decimal(18, 2) NULL DEFAULT NULL,
  `note_payable` decimal(18, 2) NULL DEFAULT NULL,
  `note_rece` decimal(18, 2) NULL DEFAULT NULL,
  `oil_gas_asset` decimal(18, 2) NULL DEFAULT NULL,
  `other_compre_income` decimal(18, 2) NULL DEFAULT NULL,
  `other_creditor_invest` decimal(18, 2) NULL DEFAULT NULL,
  `other_current_asset` decimal(18, 2) NULL DEFAULT NULL,
  `other_current_liab` decimal(18, 2) NULL DEFAULT NULL,
  `other_equity_invest` decimal(18, 2) NULL DEFAULT NULL,
  `other_equity_other` decimal(18, 2) NULL DEFAULT NULL,
  `other_equity_tool` decimal(18, 2) NULL DEFAULT NULL,
  `other_noncurrent_asset` decimal(18, 2) NULL DEFAULT NULL,
  `other_noncurrent_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `other_noncurrent_liab` decimal(18, 2) NULL DEFAULT NULL,
  `other_payable` decimal(18, 2) NULL DEFAULT NULL,
  `other_rece` decimal(18, 2) NULL DEFAULT NULL,
  `parent_equity_balance` decimal(18, 2) NULL DEFAULT NULL,
  `parent_equity_other` decimal(18, 2) NULL DEFAULT NULL,
  `perpetual_bond` decimal(18, 2) NULL DEFAULT NULL,
  `perpetual_bond_paybale` decimal(18, 2) NULL DEFAULT NULL,
  `predict_current_liab` decimal(18, 2) NULL DEFAULT NULL,
  `predict_liab` decimal(18, 2) NULL DEFAULT NULL,
  `preferred_shares` decimal(18, 2) NULL DEFAULT NULL,
  `preferred_shares_paybale` decimal(18, 2) NULL DEFAULT NULL,
  `premium_rece` decimal(18, 2) NULL DEFAULT NULL,
  `prepayment` decimal(18, 2) NULL DEFAULT NULL,
  `productive_biology_asset` decimal(18, 2) NULL DEFAULT NULL,
  `project_material` decimal(18, 2) NULL DEFAULT NULL,
  `rc_reserve_rece` decimal(18, 2) NULL DEFAULT NULL,
  `reinsure_payable` decimal(18, 2) NULL DEFAULT NULL,
  `reinsure_rece` decimal(18, 2) NULL DEFAULT NULL,
  `sell_repo_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `settle_excess_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `share_capital` decimal(18, 2) NULL DEFAULT NULL,
  `short_bond_payable` decimal(18, 2) NULL DEFAULT NULL,
  `short_fin_payable` decimal(18, 2) NULL DEFAULT NULL,
  `short_loan` decimal(18, 2) NULL DEFAULT NULL,
  `special_payable` decimal(18, 2) NULL DEFAULT NULL,
  `special_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `staff_salary_payable` decimal(18, 2) NULL DEFAULT NULL,
  `subsidy_rece` decimal(18, 2) NULL DEFAULT NULL,
  `surplus_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `tax_payable` decimal(18, 2) NULL DEFAULT NULL,
  `total_assets` decimal(18, 2) NULL DEFAULT NULL,
  `total_current_assets` decimal(18, 2) NULL DEFAULT NULL,
  `total_current_liab` decimal(18, 2) NULL DEFAULT NULL,
  `total_equity` decimal(18, 2) NULL DEFAULT NULL,
  `total_liab_equity` decimal(18, 2) NULL DEFAULT NULL,
  `total_liabilities` decimal(18, 2) NULL DEFAULT NULL,
  `total_noncurrent_assets` decimal(18, 2) NULL DEFAULT NULL,
  `total_noncurrent_liab` decimal(18, 2) NULL DEFAULT NULL,
  `total_other_payable` decimal(18, 2) NULL DEFAULT NULL,
  `total_other_rece` decimal(18, 2) NULL DEFAULT NULL,
  `total_parent_equity` decimal(18, 2) NULL DEFAULT NULL,
  `trade_finasset` decimal(18, 2) NULL DEFAULT NULL,
  `trade_finasset_notfvtpl` decimal(18, 2) NULL DEFAULT NULL,
  `trade_finliab` decimal(18, 2) NULL DEFAULT NULL,
  `trade_finliab_notfvtpl` decimal(18, 2) NULL DEFAULT NULL,
  `treasury_shares` decimal(18, 2) NULL DEFAULT NULL,
  `unassign_rpofit` decimal(18, 2) NULL DEFAULT NULL,
  `unconfirm_invest_loss` decimal(18, 2) NULL DEFAULT NULL,
  `useright_asset` decimal(18, 2) NULL DEFAULT NULL,
  `opinion_type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `secucode`(`secucode`) USING BTREE,
  INDEX `report_date`(`report_date`) USING BTREE,
  INDEX `secucode_2`(`secucode`, `report_date`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 207304 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_balancesheet_bank
-- ----------------------------
DROP TABLE IF EXISTS `em_balancesheet_bank`;
CREATE TABLE `em_balancesheet_bank`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATETYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `CURRENCY` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CASHANDDEPOSITCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `DEPOSITINFI` decimal(18, 2) NULL DEFAULT NULL,
  `PRECIOUSMETAL` decimal(18, 2) NULL DEFAULT NULL,
  `LENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DERIVEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `ACCOUNTREC` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTREC` decimal(18, 2) NULL DEFAULT NULL,
  `LOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `SALEABLEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `HELDMATURITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTREC` decimal(18, 2) NULL DEFAULT NULL,
  `LTEQUITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `INVSUBSIDIARY` decimal(18, 2) NULL DEFAULT NULL,
  `INVJOINT` decimal(18, 2) NULL DEFAULT NULL,
  `ESTATEINVEST` decimal(18, 2) NULL DEFAULT NULL,
  `FIXEDASSET` decimal(18, 2) NULL DEFAULT NULL,
  `CONSTRUCTIONPROGRESS` decimal(18, 2) NULL DEFAULT NULL,
  `INTANGIBLEASSET` decimal(18, 2) NULL DEFAULT NULL,
  `GOODWILL` decimal(18, 2) NULL DEFAULT NULL,
  `MASSET` decimal(18, 2) NULL DEFAULT NULL,
  `MASSETDEVALUE` decimal(18, 2) NULL DEFAULT NULL,
  `NETMASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXASSET` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SUMASSET` decimal(18, 2) NULL DEFAULT NULL,
  `BORROWFROMCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `FIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `BORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DERIVEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SELLBUYBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `ACCEPTDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `OUTWARDREMITTANCE` decimal(18, 2) NULL DEFAULT NULL,
  `CDANDBILLREC` decimal(18, 2) NULL DEFAULT NULL,
  `CD` decimal(18, 2) NULL DEFAULT NULL,
  `SALARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIDENDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ANTICIPATELIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `BONDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERSTOCBOND` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINBOND` decimal(18, 2) NULL DEFAULT NULL,
  `JUNIORBONDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERREDSTOCK` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINABLEDEBT` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITYOTHER` decimal(18, 2) NULL DEFAULT NULL,
  `CAPITALRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `INVREVALUERESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `INVENTORYSHARE` decimal(18, 2) NULL DEFAULT NULL,
  `HEDGERESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `SURPLUSRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `GENERALRISKPREPARE` decimal(18, 2) NULL DEFAULT NULL,
  `RETAINEDEARNING` decimal(18, 2) NULL DEFAULT NULL,
  `SUGGESTASSIGNDIVI` decimal(18, 2) NULL DEFAULT NULL,
  `DIFFCONVERSIONFC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPARENTEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIABSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1664 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_balancesheet_broker
-- ----------------------------
DROP TABLE IF EXISTS `em_balancesheet_broker`;
CREATE TABLE `em_balancesheet_broker`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATETYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `CURRENCY` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MONETARYFUND` decimal(18, 2) NULL DEFAULT NULL,
  `CLIENTFUND` decimal(18, 2) NULL DEFAULT NULL,
  `CLIENTCREDITFUND` decimal(18, 2) NULL DEFAULT NULL,
  `SETTLEMENTPROVISION` decimal(18, 2) NULL DEFAULT NULL,
  `CLIENTPROVISION` decimal(18, 2) NULL DEFAULT NULL,
  `CREDITPROVISION` decimal(18, 2) NULL DEFAULT NULL,
  `LENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `MARGINOUTFUND` decimal(18, 2) NULL DEFAULT NULL,
  `MARGINOUTSECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DERIVEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTREC` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIDENDREC` decimal(18, 2) NULL DEFAULT NULL,
  `RECEIVABLES` decimal(18, 2) NULL DEFAULT NULL,
  `GDEPOSITPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SALEABLEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `HELDMATURITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `AGENCYASSETS` decimal(18, 2) NULL DEFAULT NULL,
  `LTEQUITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `ESTATEINVEST` decimal(18, 2) NULL DEFAULT NULL,
  `FIXEDASSET` decimal(18, 2) NULL DEFAULT NULL,
  `CONSTRUCTIONPROGRESS` decimal(18, 2) NULL DEFAULT NULL,
  `INTANGIBLEASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SEATFEE` decimal(18, 2) NULL DEFAULT NULL,
  `GOODWILL` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXASSET` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SUMASSET` decimal(18, 2) NULL DEFAULT NULL,
  `STBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `PLEDGEBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `BORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DERIVEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SELLBUYBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTTRADESECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `CAGENTTRADESECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTUWSECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `ACCOUNTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SALARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIDENDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SHORTFINANCING` decimal(18, 2) NULL DEFAULT NULL,
  `AGENCYLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `ANTICIPATELIAB` decimal(18, 2) NULL DEFAULT NULL,
  `LTBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `BONDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SHARECAPITAL` decimal(18, 2) NULL DEFAULT NULL,
  `CAPITALRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `INVENTORYSHARE` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERREDSTOCK` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINABLEDEBT` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITYOTHER` decimal(18, 2) NULL DEFAULT NULL,
  `SURPLUSRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `GENERALRISKPREPARE` decimal(18, 2) NULL DEFAULT NULL,
  `TRADERISKPREPARE` decimal(18, 2) NULL DEFAULT NULL,
  `RETAINEDEARNING` decimal(18, 2) NULL DEFAULT NULL,
  `DIFFCONVERSIONFC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPARENTEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIABSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1896 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_balancesheet_common
-- ----------------------------
DROP TABLE IF EXISTS `em_balancesheet_common`;
CREATE TABLE `em_balancesheet_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `MONETARYFUND` decimal(18, 2) NULL DEFAULT NULL,
  `SETTLEMENTPROVISION` decimal(18, 2) NULL DEFAULT NULL,
  `LENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `BILLREC` decimal(18, 2) NULL DEFAULT NULL,
  `ACCOUNTREC` decimal(18, 2) NULL DEFAULT NULL,
  `ADVANCEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMREC` decimal(18, 2) NULL DEFAULT NULL,
  `RIREC` decimal(18, 2) NULL DEFAULT NULL,
  `RICONTACTRESERVEREC` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTREC` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIDENDREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREC` decimal(18, 2) NULL DEFAULT NULL,
  `EXPORTREBATEREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDYREC` decimal(18, 2) NULL DEFAULT NULL,
  `INTERNALREC` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INVENTORY` decimal(18, 2) NULL DEFAULT NULL,
  `CLHELDSALEASS` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETONEYEAR` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERLASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLASSET` decimal(18, 2) NULL DEFAULT NULL,
  `LOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `SALEABLEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `HELDMATURITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `LTREC` decimal(18, 2) NULL DEFAULT NULL,
  `LTEQUITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `ESTATEINVEST` decimal(18, 2) NULL DEFAULT NULL,
  `FIXEDASSET` decimal(18, 2) NULL DEFAULT NULL,
  `CONSTRUCTIONPROGRESS` decimal(18, 2) NULL DEFAULT NULL,
  `CONSTRUCTIONMATERIAL` decimal(18, 2) NULL DEFAULT NULL,
  `LIQUIDATEFIXEDASSET` decimal(18, 2) NULL DEFAULT NULL,
  `PRODUCTBIOLOGYASSET` decimal(18, 2) NULL DEFAULT NULL,
  `OILGASASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INTANGIBLEASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEVELOPEXP` decimal(18, 2) NULL DEFAULT NULL,
  `GOODWILL` decimal(18, 2) NULL DEFAULT NULL,
  `LTDEFERASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXASSET` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERNONLASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SUMNONLASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SUMASSET` decimal(18, 2) NULL DEFAULT NULL,
  `STBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `BORROWFROMCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `DEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `BORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `BILLPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ACCOUNTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ADVANCERECEIVE` decimal(18, 2) NULL DEFAULT NULL,
  `SELLBUYBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `COMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SALARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIDENDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `RIPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTERNALPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ANTICIPATELLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `CONTACTRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTTRADESECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTUWSECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMEONEYEAR` decimal(18, 2) NULL DEFAULT NULL,
  `STBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `CLHELDSALELIAB` decimal(18, 2) NULL DEFAULT NULL,
  `NONLLIABONEYEAR` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERLLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `LTBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `BONDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERSTOCBOND` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINBOND` decimal(18, 2) NULL DEFAULT NULL,
  `LTACCOUNTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `LTSALARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SPECIALPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ANTICIPATELIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERNONLLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SUMNONLLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SHARECAPITAL` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERREDSTOCK` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINABLEDEBT` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITYOTHER` decimal(18, 2) NULL DEFAULT NULL,
  `CAPITALRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `INVENTORYSHARE` decimal(18, 2) NULL DEFAULT NULL,
  `SPECIALRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `SURPLUSRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `GENERALRISKPREPARE` decimal(18, 2) NULL DEFAULT NULL,
  `UNCONFIRMINVLOSS` decimal(18, 2) NULL DEFAULT NULL,
  `RETAINEDEARNING` decimal(18, 2) NULL DEFAULT NULL,
  `PLANCASHDIVI` decimal(18, 2) NULL DEFAULT NULL,
  `DIFFCONVERSIONFC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPARENTEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `BILLREC_ACCOUNTREC` decimal(18, 2) NULL DEFAULT NULL,
  `BILLPAY_ACCOUNTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `CONTRACTLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREC_TOTAL` decimal(18, 2) NULL DEFAULT NULL,
  `FIXEDASSET_TOTAL` decimal(18, 2) NULL DEFAULT NULL,
  `CONSTRUCTIONPROGRESS_TOTAL` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERPAY_TOTAL` decimal(18, 2) NULL DEFAULT NULL,
  `LONGPAY_TOTAL` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIABSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `roe_weight` decimal(18, 2) NULL DEFAULT NULL,
  `monetaryfund_p` decimal(18, 2) NULL DEFAULT NULL,
  `advancepay_p` decimal(18, 2) NULL DEFAULT NULL,
  `otherrec_p` decimal(18, 2) NULL DEFAULT NULL,
  `otherlasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `ltequityinv_p` decimal(18, 2) NULL DEFAULT NULL,
  `othernonlasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `sumasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `fvaluefasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `billrec_p` decimal(18, 2) NULL DEFAULT NULL,
  `accountrec_p` decimal(18, 2) NULL DEFAULT NULL,
  `inventory_p` decimal(18, 2) NULL DEFAULT NULL,
  `saleablefasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `ltrec_p` decimal(18, 2) NULL DEFAULT NULL,
  `estateinvest_p` decimal(18, 2) NULL DEFAULT NULL,
  `fixedasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `constructionprogress_p` decimal(18, 2) NULL DEFAULT NULL,
  `intangibleasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `goodwill_p` decimal(18, 2) NULL DEFAULT NULL,
  `ltdeferasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `deferincometaxasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `stborrow_p` decimal(18, 2) NULL DEFAULT NULL,
  `billpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `accountpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `advancereceive_p` decimal(18, 2) NULL DEFAULT NULL,
  `salarypay_p` decimal(18, 2) NULL DEFAULT NULL,
  `taxpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `otherpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `nonlliaboneyear_p` decimal(18, 2) NULL DEFAULT NULL,
  `otherlliab_p` decimal(18, 2) NULL DEFAULT NULL,
  `ltborrow_p` decimal(18, 2) NULL DEFAULT NULL,
  `bondpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `ltaccountpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `deferincome_p` decimal(18, 2) NULL DEFAULT NULL,
  `deferincometaxliab_p` decimal(18, 2) NULL DEFAULT NULL,
  `sharecapital_p` decimal(18, 2) NULL DEFAULT NULL,
  `capitalreserve_p` decimal(18, 2) NULL DEFAULT NULL,
  `surplusreserve_p` decimal(18, 2) NULL DEFAULT NULL,
  `retainedearning_p` decimal(18, 2) NULL DEFAULT NULL,
  `minorityequity_p` decimal(18, 2) NULL DEFAULT NULL,
  `sumparentequity_p` decimal(18, 2) NULL DEFAULT NULL,
  `sumshequity_p` decimal(18, 2) NULL DEFAULT NULL,
  `contractliab_p` decimal(18, 2) NULL DEFAULT NULL,
  `heldmaturityinv_p` decimal(18, 2) NULL DEFAULT NULL,
  `definefvaluefasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `tradefliab_p` decimal(18, 2) NULL DEFAULT NULL,
  `interestpay_p` decimal(18, 2) NULL DEFAULT NULL,
  `nonlassetoneyear_p` decimal(18, 2) NULL DEFAULT NULL,
  `tradefasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `sumlasset_p` decimal(18, 2) NULL DEFAULT NULL,
  `sumlliab_p` decimal(18, 2) NULL DEFAULT NULL,
  `sustainabledebt_p` decimal(18, 2) NULL DEFAULT NULL,
  `debtinv` decimal(18, 2) NULL DEFAULT NULL,
  `debtinv_p` decimal(18, 2) NULL DEFAULT NULL,
  `kf_roe` decimal(18, 2) NULL DEFAULT NULL,
  `bonus_preyear` decimal(18, 2) NULL DEFAULT NULL,
  `interest_exchange` decimal(18, 2) NULL DEFAULT NULL,
  `interest_income` decimal(18, 2) NULL DEFAULT NULL,
  `contractasset` decimal(18, 2) NULL DEFAULT NULL,
  `account_rec_fina` decimal(18, 2) NULL DEFAULT NULL,
  `otherequity_invest` decimal(18, 2) NULL DEFAULT NULL,
  `othernonlfinasset` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_id_bal_com`(`id`) USING BTREE,
  INDEX `idx_roe`(`roe_weight`) USING BTREE,
  INDEX `idx_tscode`(`SECURITYCODE`) USING BTREE,
  INDEX `REPORTDATE`(`REPORTDATE`) USING BTREE,
  INDEX `SECURITYCODE`(`SECURITYCODE`, `REPORTDATE`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 198271 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_balancesheet_insurance
-- ----------------------------
DROP TABLE IF EXISTS `em_balancesheet_insurance`;
CREATE TABLE `em_balancesheet_insurance`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATETYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `CURRENCY` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MONETARYFUND` decimal(18, 2) NULL DEFAULT NULL,
  `SETTLEMENTPROVISION` decimal(18, 2) NULL DEFAULT NULL,
  `LENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `DERIVEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTREC` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMREC` decimal(18, 2) NULL DEFAULT NULL,
  `RIREC` decimal(18, 2) NULL DEFAULT NULL,
  `RICONTACTRESERVEREC` decimal(18, 2) NULL DEFAULT NULL,
  `UNDUERIRESERVEREC` decimal(18, 2) NULL DEFAULT NULL,
  `CLAIMRIRESERVEREC` decimal(18, 2) NULL DEFAULT NULL,
  `LIFERIRESERVEREC` decimal(18, 2) NULL DEFAULT NULL,
  `LTHEALTHRIRESERVEREC` decimal(18, 2) NULL DEFAULT NULL,
  `INSUREDPLEDGELOAN` decimal(18, 2) NULL DEFAULT NULL,
  `LOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `GDEPOSITPAY` decimal(18, 2) NULL DEFAULT NULL,
  `CREDITORPLANINV` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREC` decimal(18, 2) NULL DEFAULT NULL,
  `TDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `SALEABLEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `HELDMATURITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTREC` decimal(18, 2) NULL DEFAULT NULL,
  `ACCOUNTREC` decimal(18, 2) NULL DEFAULT NULL,
  `LTEQUITYINV` decimal(18, 2) NULL DEFAULT NULL,
  `CAPITALGDEPOSITPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ESTATEINVEST` decimal(18, 2) NULL DEFAULT NULL,
  `FIXEDASSET` decimal(18, 2) NULL DEFAULT NULL,
  `CONSTRUCTIONPROGRESS` decimal(18, 2) NULL DEFAULT NULL,
  `INTANGIBLEASSET` decimal(18, 2) NULL DEFAULT NULL,
  `GOODWILL` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXASSET` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INDEPENDENTASSET` decimal(18, 2) NULL DEFAULT NULL,
  `SUMASSET` decimal(18, 2) NULL DEFAULT NULL,
  `STBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `FIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `GDEPOSITREC` decimal(18, 2) NULL DEFAULT NULL,
  `BORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `TRADEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DEFINEFVALUEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `DERIVEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SELLBUYBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `ACCEPTDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTTRADESECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `ACCOUNTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BILLPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ADVANCEREC` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMADVANCE` decimal(18, 2) NULL DEFAULT NULL,
  `COMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `RIPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SALARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTERESTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIDENDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ANTICIPATELIAB` decimal(18, 2) NULL DEFAULT NULL,
  `CLAIMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `POLICYDIVIPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INSUREDDEPOSITINV` decimal(18, 2) NULL DEFAULT NULL,
  `CONTACTRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `UNDUERESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `CLAIMRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `LIFERESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `LTHEALTHRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `LTBORROW` decimal(18, 2) NULL DEFAULT NULL,
  `BONDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERSTOCBOND` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINBOND` decimal(18, 2) NULL DEFAULT NULL,
  `JUNIORBONDPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DEFERINCOMETAXLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `INDEPENDENTLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `SHARECAPITAL` decimal(18, 2) NULL DEFAULT NULL,
  `CAPITALRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `PREFERREDSTOCK` decimal(18, 2) NULL DEFAULT NULL,
  `SUSTAINABLEDEBT` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREQUITYOTHER` decimal(18, 2) NULL DEFAULT NULL,
  `SURPLUSRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `GENERALRISKPREPARE` decimal(18, 2) NULL DEFAULT NULL,
  `RETAINEDEARNING` decimal(18, 2) NULL DEFAULT NULL,
  `DIFFCONVERSIONFC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPARENTEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMLIABSHEQUITY` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 281 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_cashflow_bank
-- ----------------------------
DROP TABLE IF EXISTS `em_cashflow_bank`;
CREATE TABLE `em_cashflow_bank`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `NIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `NICLIENTDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `NIFIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWFROMCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `NDDEPOSITINCBANKFI` decimal(18, 2) NULL DEFAULT NULL,
  `NDDEPOSITINCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `NDDEPOSITINFI` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWSELLBUYBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NISELLBUYBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NDLENDBUYSELLBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NDLENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NDBUYSELLBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NETCD` decimal(18, 2) NULL DEFAULT NULL,
  `NITRADEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `NDTRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMREC` decimal(18, 2) NULL DEFAULT NULL,
  `INTREC` decimal(18, 2) NULL DEFAULT NULL,
  `COMMREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPMASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `CANCELLOANREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `NILOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `NDBORROWFROMCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `NIDEPOSITINCBANKFI` decimal(18, 2) NULL DEFAULT NULL,
  `NIDEPOSITINCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `NIDEPOSITINFI` decimal(18, 2) NULL DEFAULT NULL,
  `NDFIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `NDISSUECD` decimal(18, 2) NULL DEFAULT NULL,
  `NILENDSELLBUYBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NILENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NIBUYSELLBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NDBORROWSELLBUYBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NDBORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NDSELLBUYBACKFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NITRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NDTRADEFLIAB` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `COMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `EMPLOYEEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BUYFINALEASEASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NIACCOUNTREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETOPERATECASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `DISPOSALINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `INVINCOMEREC` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIORPROFITREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPFILASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPSUBSIDIARYREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `INVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BUYFILASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `GETSUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETINVCASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUEBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUEJUNIORBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUEOTHERBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAREC` decimal(18, 2) NULL DEFAULT NULL,
  `ACCEPTINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDIARYACCEPT` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUECD` decimal(18, 2) NULL DEFAULT NULL,
  `ADDSHARECAPITALREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `REPAYDEBTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BONDINTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUESHAREREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIPROFITORINTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETFINACASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `EFFECTEXCHANGERATE` decimal(18, 2) NULL DEFAULT NULL,
  `NICASHEQUI` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIBEGINNING` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIENDING` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1610 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_cashflow_broker
-- ----------------------------
DROP TABLE IF EXISTS `em_cashflow_broker`;
CREATE TABLE `em_cashflow_broker`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATETYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `CURRENCY` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `NIDISPTRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NIOTHERFINAINSTRU` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMREC` decimal(18, 2) NULL DEFAULT NULL,
  `UWSECURITYREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTTRADESECURITYREC` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTUWSECURITYREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIBUYBACKFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NITRADESETTLEMENT` decimal(18, 2) NULL DEFAULT NULL,
  `NIDIRECTINV` decimal(18, 2) NULL DEFAULT NULL,
  `TAXRETURNREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NDDISPTRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `NDOTHERFINAINSTR` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NDBORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `EMPLOYEEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NDTRADESETTLEMENT` decimal(18, 2) NULL DEFAULT NULL,
  `NDDIRECTINV` decimal(18, 2) NULL DEFAULT NULL,
  `NILENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NDBUYBACKFUND` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTTRADESECURITYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETOPERATECASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `DISPOSALINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIDISPSALEABLEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INVINCOMEREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPFILASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPSUBSIDIARYREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `INVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NDDISPSALEABLEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `BUYFILASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `GETSUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETINVCASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `ACCEPTINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDIARYACCEPT` decimal(18, 2) NULL DEFAULT NULL,
  `LOANREC` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUEBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `REPAYDEBTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIPROFITORINTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETFINACASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `EFFECTEXCHANGERATE` decimal(18, 2) NULL DEFAULT NULL,
  `NICASHEQUI` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIBEGINNING` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIENDING` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1748 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_cashflow_common
-- ----------------------------
DROP TABLE IF EXISTS `em_cashflow_common`;
CREATE TABLE `em_cashflow_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `SALEGOODSSERVICEREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWFROMCBANK` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWFROMFI` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMREC` decimal(18, 2) NULL DEFAULT NULL,
  `NETRIREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIINSUREDDEPOSITINV` decimal(18, 2) NULL DEFAULT NULL,
  `NIDISPTRADEFASSET` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIBORROWFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NDLOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `NIBUYBACKFUND` decimal(18, 2) NULL DEFAULT NULL,
  `TAXRETURNREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `BUYGOODSSERVICEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NILOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `NIDEPOSITINCBANKFI` decimal(18, 2) NULL DEFAULT NULL,
  `INDEMNITYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIPAY` decimal(18, 2) NULL DEFAULT NULL,
  `EMPLOYEEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETOPERATECASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `DISPOSALINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `INVINCOMEREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPFILASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPSUBSIDIARYREC` decimal(18, 2) NULL DEFAULT NULL,
  `REDUCEPLEDGETDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `BUYFILASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `INVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NIPLEDGELOAN` decimal(18, 2) NULL DEFAULT NULL,
  `GETSUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `ADDPLEDGETDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETINVCASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `ACCEPTINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDIARYACCEPT` decimal(18, 2) NULL DEFAULT NULL,
  `LOANREC` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUEBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `REPAYDEBTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIPROFITORINTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUBSIDIARYREDUCTCAPITAL` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETFINACASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `EFFECTEXCHANGERATE` decimal(18, 2) NULL DEFAULT NULL,
  `NICASHEQUI` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIBEGINNING` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIENDING` decimal(18, 2) NULL DEFAULT NULL,
  `unreinveloss` decimal(18, 2) NULL DEFAULT NULL,
  `asseimpa` decimal(18, 2) NULL DEFAULT NULL,
  `creditimpa` decimal(18, 2) NULL DEFAULT NULL,
  `assedepr` decimal(18, 2) NULL DEFAULT NULL,
  `realestadep` decimal(18, 2) NULL DEFAULT NULL,
  `intaasseamor` decimal(18, 2) NULL DEFAULT NULL,
  `longdefeexpenamor` decimal(18, 2) NULL DEFAULT NULL,
  `prepexpedecr` decimal(18, 2) NULL DEFAULT NULL,
  `accrexpeincr` decimal(18, 2) NULL DEFAULT NULL,
  `dispfixedassetloss` decimal(18, 2) NULL DEFAULT NULL,
  `fixedassescraloss` decimal(18, 2) NULL DEFAULT NULL,
  `valuechgloss` decimal(18, 2) NULL DEFAULT NULL,
  `defeincoincr` decimal(18, 2) NULL DEFAULT NULL,
  `estidebts` decimal(18, 2) NULL DEFAULT NULL,
  `finexpe` decimal(18, 2) NULL DEFAULT NULL,
  `inveloss` decimal(18, 2) NULL DEFAULT NULL,
  `defetaxassetdecr` decimal(18, 2) NULL DEFAULT NULL,
  `defetaxliabincr` decimal(18, 2) NULL DEFAULT NULL,
  `inveredu` decimal(18, 2) NULL DEFAULT NULL,
  `receredu` decimal(18, 2) NULL DEFAULT NULL,
  `payaincr` decimal(18, 2) NULL DEFAULT NULL,
  `unseparachg` decimal(18, 2) NULL DEFAULT NULL,
  `unfiparachg` decimal(18, 2) NULL DEFAULT NULL,
  `debtintocapi` decimal(18, 2) NULL DEFAULT NULL,
  `expiconvbd` decimal(18, 2) NULL DEFAULT NULL,
  `finfixedasset` decimal(18, 2) NULL DEFAULT NULL,
  `netoperatecashflow_p` decimal(18, 2) NULL DEFAULT NULL,
  `bonus` decimal(18, 2) NULL DEFAULT NULL,
  `bonus_date` date NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `SECURITYCODE`(`SECURITYCODE`) USING BTREE,
  INDEX `REPORTDATE`(`REPORTDATE`) USING BTREE,
  INDEX `SECURITYCODE_2`(`SECURITYCODE`, `REPORTDATE`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 194781 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_cashflow_insurance
-- ----------------------------
DROP TABLE IF EXISTS `em_cashflow_insurance`;
CREATE TABLE `em_cashflow_insurance`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATETYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `CURRENCY` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `NIDEPOSIT` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMREC` decimal(18, 2) NULL DEFAULT NULL,
  `NETRIREC` decimal(18, 2) NULL DEFAULT NULL,
  `NIINSUREDDEPOSITINV` decimal(18, 2) NULL DEFAULT NULL,
  `TAXRETURNREC` decimal(18, 2) NULL DEFAULT NULL,
  `NETTRADEFASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMREC` decimal(18, 2) NULL DEFAULT NULL,
  `NILENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NDDEPOSITINCBANKFI` decimal(18, 2) NULL DEFAULT NULL,
  `NISELLBUYBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NDBUYSELLBACK` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `INDEMNITYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NETRIPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NDLENDFUND` decimal(18, 2) NULL DEFAULT NULL,
  `NIBUYSELLBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NDSELLBUYBACK` decimal(18, 2) NULL DEFAULT NULL,
  `NILOANADVANCES` decimal(18, 2) NULL DEFAULT NULL,
  `INTANDCOMMPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NDINSUREDDEPOSITINV` decimal(18, 2) NULL DEFAULT NULL,
  `NIDEPOSITINCBANKFI` decimal(18, 2) NULL DEFAULT NULL,
  `EMPLOYEEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `TAXPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NETTRADEFASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEROPERATEPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMOPERATEFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETOPERATECASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `DISPOSALINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `INVINCOMEREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPFILASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `DISPSUBSIDIARYREC` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `INVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NIINSUREDPLEDGELOAN` decimal(18, 2) NULL DEFAULT NULL,
  `BUYFILASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DISPSUBSIDIARYPAY` decimal(18, 2) NULL DEFAULT NULL,
  `BUYSELLBACKFASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERINVPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMINVFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETINVCASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `ACCEPTINVREC` decimal(18, 2) NULL DEFAULT NULL,
  `LOANREC` decimal(18, 2) NULL DEFAULT NULL,
  `ISSUEBONDREC` decimal(18, 2) NULL DEFAULT NULL,
  `NETSELLBUYBACKFASSETREC` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAREC` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWIN` decimal(18, 2) NULL DEFAULT NULL,
  `REPAYDEBTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `DIVIPROFITORINTPAY` decimal(18, 2) NULL DEFAULT NULL,
  `NETSELLBUYBACKFASSETPAY` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERFINAPAY` decimal(18, 2) NULL DEFAULT NULL,
  `SUMFINAFLOWOUT` decimal(18, 2) NULL DEFAULT NULL,
  `NETFINACASHFLOW` decimal(18, 2) NULL DEFAULT NULL,
  `EFFECTEXCHANGERATE` decimal(18, 2) NULL DEFAULT NULL,
  `NICASHEQUI` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIBEGINNING` decimal(18, 2) NULL DEFAULT NULL,
  `CASHEQUIENDING` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 294 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_cf_common_v1
-- ----------------------------
DROP TABLE IF EXISTS `em_cf_common_v1`;
CREATE TABLE `em_cf_common_v1`  (
  `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT,
  `secucode` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `security_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `report_date` date NULL DEFAULT NULL,
  `notice_date` date NULL DEFAULT NULL,
  `update_date` date NULL DEFAULT NULL,
  `sales_services` decimal(18, 2) NULL DEFAULT NULL,
  `deposit_interbank_add` decimal(18, 2) NULL DEFAULT NULL,
  `loan_pbc_add` decimal(18, 2) NULL DEFAULT NULL,
  `ofi_bf_add` decimal(18, 2) NULL DEFAULT NULL,
  `receive_origic_premium` decimal(18, 2) NULL DEFAULT NULL,
  `receive_reinsure_net` decimal(18, 2) NULL DEFAULT NULL,
  `insured_invest_add` decimal(18, 2) NULL DEFAULT NULL,
  `disposal_tfa_add` decimal(18, 2) NULL DEFAULT NULL,
  `receive_interest_commission` decimal(18, 2) NULL DEFAULT NULL,
  `borrow_fund_add` decimal(18, 2) NULL DEFAULT NULL,
  `loan_advance_reduce` decimal(18, 2) NULL DEFAULT NULL,
  `repo_business_add` decimal(18, 2) NULL DEFAULT NULL,
  `receive_tax_refund` decimal(18, 2) NULL DEFAULT NULL,
  `receive_other_operate` decimal(18, 2) NULL DEFAULT NULL,
  `operate_inflow_other` decimal(18, 2) NULL DEFAULT NULL,
  `operate_inflow_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_operate_inflow` decimal(18, 2) NULL DEFAULT NULL,
  `buy_services` decimal(18, 2) NULL DEFAULT NULL,
  `loan_advance_add` decimal(18, 2) NULL DEFAULT NULL,
  `pbc_interbank_add` decimal(18, 2) NULL DEFAULT NULL,
  `pay_origic_compensate` decimal(18, 2) NULL DEFAULT NULL,
  `pay_interest_commission` decimal(18, 2) NULL DEFAULT NULL,
  `pay_policy_bonus` decimal(18, 2) NULL DEFAULT NULL,
  `pay_staff_cash` decimal(18, 2) NULL DEFAULT NULL,
  `pay_all_tax` decimal(18, 2) NULL DEFAULT NULL,
  `pay_other_operate` decimal(18, 2) NULL DEFAULT NULL,
  `operate_outflow_other` decimal(18, 2) NULL DEFAULT NULL,
  `operate_outflow_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_operate_outflow` decimal(18, 2) NULL DEFAULT NULL,
  `operate_netcash_other` decimal(18, 2) NULL DEFAULT NULL,
  `operate_netcash_balance` decimal(18, 2) NULL DEFAULT NULL,
  `netcash_operate` decimal(18, 2) NULL DEFAULT NULL,
  `withdraw_invest` decimal(18, 2) NULL DEFAULT NULL,
  `receive_invest_income` decimal(18, 2) NULL DEFAULT NULL,
  `disposal_long_asset` decimal(18, 2) NULL DEFAULT NULL,
  `disposal_subsidiary_other` decimal(18, 2) NULL DEFAULT NULL,
  `reduce_pledge_timedeposits` decimal(18, 2) NULL DEFAULT NULL,
  `receive_other_invest` decimal(18, 2) NULL DEFAULT NULL,
  `invest_inflow_other` decimal(18, 2) NULL DEFAULT NULL,
  `invest_inflow_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_invest_inflow` decimal(18, 2) NULL DEFAULT NULL,
  `construct_long_asset` decimal(18, 2) NULL DEFAULT NULL,
  `invest_pay_cash` decimal(18, 2) NULL DEFAULT NULL,
  `pledge_loan_add` decimal(18, 2) NULL DEFAULT NULL,
  `obtain_subsidiary_other` decimal(18, 2) NULL DEFAULT NULL,
  `add_pledge_timedeposits` decimal(18, 2) NULL DEFAULT NULL,
  `pay_other_invest` decimal(18, 2) NULL DEFAULT NULL,
  `invest_outflow_other` decimal(18, 2) NULL DEFAULT NULL,
  `invest_outflow_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_invest_outflow` decimal(18, 2) NULL DEFAULT NULL,
  `invest_netcash_other` decimal(18, 2) NULL DEFAULT NULL,
  `invest_netcash_balance` decimal(18, 2) NULL DEFAULT NULL,
  `netcash_invest` decimal(18, 2) NULL DEFAULT NULL,
  `accept_invest_cash` decimal(18, 2) NULL DEFAULT NULL,
  `subsidiary_accept_invest` decimal(18, 2) NULL DEFAULT NULL,
  `receive_loan_cash` decimal(18, 2) NULL DEFAULT NULL,
  `issue_bond` decimal(18, 2) NULL DEFAULT NULL,
  `receive_other_finance` decimal(18, 2) NULL DEFAULT NULL,
  `finance_inflow_other` decimal(18, 2) NULL DEFAULT NULL,
  `finance_inflow_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_finance_inflow` decimal(18, 2) NULL DEFAULT NULL,
  `pay_debt_cash` decimal(18, 2) NULL DEFAULT NULL,
  `assign_dividend_porfit` decimal(18, 2) NULL DEFAULT NULL,
  `subsidiary_pay_dividend` decimal(18, 2) NULL DEFAULT NULL,
  `buy_subsidiary_equity` decimal(18, 2) NULL DEFAULT NULL,
  `pay_other_finance` decimal(18, 2) NULL DEFAULT NULL,
  `subsidiary_reduce_cash` decimal(18, 2) NULL DEFAULT NULL,
  `finance_outflow_other` decimal(18, 2) NULL DEFAULT NULL,
  `finance_outflow_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_finance_outflow` decimal(18, 2) NULL DEFAULT NULL,
  `finance_netcash_other` decimal(18, 2) NULL DEFAULT NULL,
  `finance_netcash_balance` decimal(18, 2) NULL DEFAULT NULL,
  `netcash_finance` decimal(18, 2) NULL DEFAULT NULL,
  `rate_change_effect` decimal(18, 2) NULL DEFAULT NULL,
  `cce_add_other` decimal(18, 2) NULL DEFAULT NULL,
  `cce_add_balance` decimal(18, 2) NULL DEFAULT NULL,
  `cce_add` decimal(18, 2) NULL DEFAULT NULL,
  `begin_cce` decimal(18, 2) NULL DEFAULT NULL,
  `end_cce_other` decimal(18, 2) NULL DEFAULT NULL,
  `end_cce_balance` decimal(18, 2) NULL DEFAULT NULL,
  `end_cce` decimal(18, 2) NULL DEFAULT NULL,
  `netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `asset_impairment` decimal(18, 2) NULL DEFAULT NULL,
  `fa_ir_depr` decimal(18, 2) NULL DEFAULT NULL,
  `oilgas_biology_depr` decimal(18, 2) NULL DEFAULT NULL,
  `ir_depr` decimal(18, 2) NULL DEFAULT NULL,
  `ia_amortize` decimal(18, 2) NULL DEFAULT NULL,
  `lpe_amortize` decimal(18, 2) NULL DEFAULT NULL,
  `defer_income_amortize` decimal(18, 2) NULL DEFAULT NULL,
  `prepaid_expense_reduce` decimal(18, 2) NULL DEFAULT NULL,
  `accrued_expense_add` decimal(18, 2) NULL DEFAULT NULL,
  `disposal_longasset_loss` decimal(18, 2) NULL DEFAULT NULL,
  `fa_scrap_loss` decimal(18, 2) NULL DEFAULT NULL,
  `fairvalue_change_loss` decimal(18, 2) NULL DEFAULT NULL,
  `finance_expense` decimal(18, 2) NULL DEFAULT NULL,
  `invest_loss` decimal(18, 2) NULL DEFAULT NULL,
  `defer_tax` decimal(18, 2) NULL DEFAULT NULL,
  `dt_asset_reduce` decimal(18, 2) NULL DEFAULT NULL,
  `dt_liab_add` decimal(18, 2) NULL DEFAULT NULL,
  `predict_liab_add` decimal(18, 2) NULL DEFAULT NULL,
  `inventory_reduce` decimal(18, 2) NULL DEFAULT NULL,
  `operate_rece_reduce` decimal(18, 2) NULL DEFAULT NULL,
  `operate_payable_add` decimal(18, 2) NULL DEFAULT NULL,
  `other` decimal(18, 2) NULL DEFAULT NULL,
  `operate_netcash_othernote` decimal(18, 2) NULL DEFAULT NULL,
  `operate_netcash_balancenote` decimal(18, 2) NULL DEFAULT NULL,
  `netcash_operatenote` decimal(18, 2) NULL DEFAULT NULL,
  `debt_transfer_capital` decimal(18, 2) NULL DEFAULT NULL,
  `convert_bond_1year` decimal(18, 2) NULL DEFAULT NULL,
  `finlease_obtain_fa` decimal(18, 2) NULL DEFAULT NULL,
  `uninvolve_investfin_other` decimal(18, 2) NULL DEFAULT NULL,
  `end_cash` decimal(18, 2) NULL DEFAULT NULL,
  `begin_cash` decimal(18, 2) NULL DEFAULT NULL,
  `end_cash_equivalents` decimal(18, 2) NULL DEFAULT NULL,
  `begin_cash_equivalents` decimal(18, 2) NULL DEFAULT NULL,
  `cce_add_othernote` decimal(18, 2) NULL DEFAULT NULL,
  `cce_add_balancenote` decimal(18, 2) NULL DEFAULT NULL,
  `cce_addnote` decimal(18, 2) NULL DEFAULT NULL,
  `opinion_type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `minority_interest` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `secucode`(`secucode`) USING BTREE,
  INDEX `report_date`(`report_date`) USING BTREE,
  INDEX `secucode_2`(`secucode`, `report_date`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 203584 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_core_concept
-- ----------------------------
DROP TABLE IF EXISTS `em_core_concept`;
CREATE TABLE `em_core_concept`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `ts_code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `board_name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `is_precise` int(10) NULL DEFAULT NULL,
  `pinyin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `fullpinyin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `board_name`(`board_name`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 18014 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_hk_balsheet_common
-- ----------------------------
DROP TABLE IF EXISTS `em_hk_balsheet_common`;
CREATE TABLE `em_hk_balsheet_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `enddate` date NULL DEFAULT NULL,
  `xjjxjdjw` decimal(18, 4) NULL DEFAULT NULL,
  `sxzckjxj` decimal(18, 4) NULL DEFAULT NULL,
  `ld_ygyjzjlljrzc` decimal(18, 4) NULL DEFAULT NULL,
  `ld_ysjrzc` decimal(18, 4) NULL DEFAULT NULL,
  `ld_kgcsjrzc` decimal(18, 4) NULL DEFAULT NULL,
  `ld_cyzdqtz` decimal(18, 4) NULL DEFAULT NULL,
  `yszkjpj` decimal(18, 4) NULL DEFAULT NULL,
  `ysglgskx` decimal(18, 4) NULL DEFAULT NULL,
  `yfkx_ld_qtyskx` decimal(18, 4) NULL DEFAULT NULL,
  `kshbqsx` decimal(18, 4) NULL DEFAULT NULL,
  `ch` decimal(18, 4) NULL DEFAULT NULL,
  `ldzcqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `ldzchj` decimal(18, 4) NULL DEFAULT NULL,
  `wycfjsb` decimal(18, 4) NULL DEFAULT NULL,
  `tzwy` decimal(18, 4) NULL DEFAULT NULL,
  `yfkx_fld_qtyskx` decimal(18, 4) NULL DEFAULT NULL,
  `tdsyq` decimal(18, 4) NULL DEFAULT NULL,
  `syjwxzc` decimal(18, 4) NULL DEFAULT NULL,
  `sy` decimal(18, 4) NULL DEFAULT NULL,
  `wxzc` decimal(18, 4) NULL DEFAULT NULL,
  `ylyhhygstz` decimal(18, 4) NULL DEFAULT NULL,
  `yfsgstz` decimal(18, 4) NULL DEFAULT NULL,
  `fld_ygyjzjlljrzc` decimal(18, 4) NULL DEFAULT NULL,
  `fld_ysjrzc` decimal(18, 4) NULL DEFAULT NULL,
  `fld_kgcsjrzc` decimal(18, 4) NULL DEFAULT NULL,
  `fld_cyzdqtz` decimal(18, 4) NULL DEFAULT NULL,
  `dysxzc` decimal(18, 4) NULL DEFAULT NULL,
  `fldzcqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `fldzchj` decimal(18, 4) NULL DEFAULT NULL,
  `zcze` decimal(18, 4) NULL DEFAULT NULL,
  `dqjk` decimal(18, 4) NULL DEFAULT NULL,
  `ld_rzzlfz` decimal(18, 4) NULL DEFAULT NULL,
  `ld_ygyjzjljrfz` decimal(18, 4) NULL DEFAULT NULL,
  `ld_jrfz` decimal(18, 4) NULL DEFAULT NULL,
  `yfzkjpj` decimal(18, 4) NULL DEFAULT NULL,
  `qtyfkxjyjfy` decimal(18, 4) NULL DEFAULT NULL,
  `yfsx` decimal(18, 4) NULL DEFAULT NULL,
  `yfgxjlx` decimal(18, 4) NULL DEFAULT NULL,
  `ld_dysr` decimal(18, 4) NULL DEFAULT NULL,
  `ldfzqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `ldfzhj` decimal(18, 4) NULL DEFAULT NULL,
  `ldzcjz` decimal(18, 4) NULL DEFAULT NULL,
  `zzcjldfz` decimal(18, 4) NULL DEFAULT NULL,
  `cqjk` decimal(18, 4) NULL DEFAULT NULL,
  `fld_rzzlfz` decimal(18, 4) NULL DEFAULT NULL,
  `fld_ygyjzjljrfz` decimal(18, 4) NULL DEFAULT NULL,
  `fld_ysjrfz` decimal(18, 4) NULL DEFAULT NULL,
  `dysxfz` decimal(18, 4) NULL DEFAULT NULL,
  `fld_dysr` decimal(18, 4) NULL DEFAULT NULL,
  `fldfzqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `fldfzhj` decimal(18, 4) NULL DEFAULT NULL,
  `fzze` decimal(18, 4) NULL DEFAULT NULL,
  `gb` decimal(18, 4) NULL DEFAULT NULL,
  `cb` decimal(18, 4) NULL DEFAULT NULL,
  `gbyj` decimal(18, 4) NULL DEFAULT NULL,
  `lcsy` decimal(18, 4) NULL DEFAULT NULL,
  `qtcb` decimal(18, 4) NULL DEFAULT NULL,
  `npgx` decimal(18, 4) NULL DEFAULT NULL,
  `gsymgsgdqyqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `gsymgsgdqy` decimal(18, 4) NULL DEFAULT NULL,
  `fkgqy` decimal(18, 4) NULL DEFAULT NULL,
  `gdqyqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `gdqyhj` decimal(18, 4) NULL DEFAULT NULL,
  `fzjgdqy` decimal(18, 4) NULL DEFAULT NULL,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `enddate`(`enddate`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 25990 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_hk_cashflow_common
-- ----------------------------
DROP TABLE IF EXISTS `em_hk_cashflow_common`;
CREATE TABLE `em_hk_cashflow_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `enddate` date NULL DEFAULT NULL,
  `csqlr` decimal(18, 4) NULL DEFAULT NULL,
  `zcjzzb` decimal(18, 4) NULL DEFAULT NULL,
  `zjytx` decimal(18, 4) NULL DEFAULT NULL,
  `cswycfjsbdks` decimal(18, 4) NULL DEFAULT NULL,
  `tzkssy` decimal(18, 4) NULL DEFAULT NULL,
  `yzlyjhygskssy` decimal(18, 4) NULL DEFAULT NULL,
  `cgyy` decimal(18, 4) NULL DEFAULT NULL,
  `lxzc` decimal(18, 4) NULL DEFAULT NULL,
  `lxsr` decimal(18, 4) NULL DEFAULT NULL,
  `chdjszj` decimal(18, 4) NULL DEFAULT NULL,
  `yszkjszj` decimal(18, 4) NULL DEFAULT NULL,
  `yfkxjqtyskjszj` decimal(18, 4) NULL DEFAULT NULL,
  `yfzkzjjs` decimal(18, 4) NULL DEFAULT NULL,
  `yszkjqtyfkzjjs` decimal(18, 4) NULL DEFAULT NULL,
  `jyzjbdqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `jyhdcsdxj` decimal(18, 4) NULL DEFAULT NULL,
  `yslxjy` decimal(18, 4) NULL DEFAULT NULL,
  `yflxjy` decimal(18, 4) NULL DEFAULT NULL,
  `yfsx` decimal(18, 4) NULL DEFAULT NULL,
  `jyhdcscjljeqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `jyhdcscjlje` decimal(18, 4) NULL DEFAULT NULL,
  `gmwycfjsbzfdxj` decimal(18, 4) NULL DEFAULT NULL,
  `cswycfjsbsddxj` decimal(18, 4) NULL DEFAULT NULL,
  `gmwxzcjqtzczfdxj` decimal(18, 4) NULL DEFAULT NULL,
  `cswxzcjqtzcsdxj` decimal(18, 4) NULL DEFAULT NULL,
  `gmzgslyqyjhyqyzfdxj` decimal(18, 4) NULL DEFAULT NULL,
  `cszgslyqyjhyqyzfdxj` decimal(18, 4) NULL DEFAULT NULL,
  `gmzqtzszfdxj` decimal(18, 4) NULL DEFAULT NULL,
  `cszqtzssddxj` decimal(18, 4) NULL DEFAULT NULL,
  `yslxjgxtx` decimal(18, 4) NULL DEFAULT NULL,
  `tzhdcsdxjlljeqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `tzhdcsdxjllje` decimal(18, 4) NULL DEFAULT NULL,
  `xzjk` decimal(18, 4) NULL DEFAULT NULL,
  `chjk` decimal(18, 4) NULL DEFAULT NULL,
  `xstzsd` decimal(18, 4) NULL DEFAULT NULL,
  `fzgf` decimal(18, 4) NULL DEFAULT NULL,
  `hggf` decimal(18, 4) NULL DEFAULT NULL,
  `fxzq` decimal(18, 4) NULL DEFAULT NULL,
  `ghchzq` decimal(18, 4) NULL DEFAULT NULL,
  `fxfy` decimal(18, 4) NULL DEFAULT NULL,
  `yfgxrz` decimal(18, 4) NULL DEFAULT NULL,
  `yflxrz` decimal(18, 4) NULL DEFAULT NULL,
  `rzhdcsdxjlljeqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `rzhdcsdxjllje` decimal(18, 4) NULL DEFAULT NULL,
  `xjjxjjdjwjzjeqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `xjjxjjdjwjzje` decimal(18, 4) NULL DEFAULT NULL,
  `xjjxjdjwdqcye` decimal(18, 4) NULL DEFAULT NULL,
  `hlbddxjjxjdjwdyx` decimal(18, 4) NULL DEFAULT NULL,
  `xjjxjdjwdqmyeqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `xjjxjdjwdqmye` decimal(18, 4) NULL DEFAULT NULL,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `enddate`(`enddate`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27321 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_hk_income_common
-- ----------------------------
DROP TABLE IF EXISTS `em_hk_income_common`;
CREATE TABLE `em_hk_income_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `enddate` date NULL DEFAULT NULL,
  `yysr` decimal(18, 4) NULL DEFAULT NULL,
  `xscb` decimal(18, 4) NULL DEFAULT NULL,
  `ml` decimal(18, 4) NULL DEFAULT NULL,
  `qtsr` decimal(18, 4) NULL DEFAULT NULL,
  `xsjfxcb` decimal(18, 4) NULL DEFAULT NULL,
  `xzkz` decimal(18, 4) NULL DEFAULT NULL,
  `ygxc` decimal(18, 4) NULL DEFAULT NULL,
  `yffy` decimal(18, 4) NULL DEFAULT NULL,
  `zjhtx` decimal(18, 4) NULL DEFAULT NULL,
  `qtzc` decimal(18, 4) NULL DEFAULT NULL,
  `qcjzss` decimal(18, 4) NULL DEFAULT NULL,
  `cgyy` decimal(18, 4) NULL DEFAULT NULL,
  `cszczyl` decimal(18, 4) NULL DEFAULT NULL,
  `jyyl` decimal(18, 4) NULL DEFAULT NULL,
  `yzlygsyl` decimal(18, 4) NULL DEFAULT NULL,
  `yzhygsyl` decimal(18, 4) NULL DEFAULT NULL,
  `cwcb` decimal(18, 4) NULL DEFAULT NULL,
  `yxsqlrdqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `sqlr` decimal(18, 4) NULL DEFAULT NULL,
  `sds` decimal(18, 4) NULL DEFAULT NULL,
  `yxjlddqtxm` decimal(18, 4) NULL DEFAULT NULL,
  `jlr` decimal(18, 4) NULL DEFAULT NULL,
  `bgsyyryzjlr` decimal(18, 4) NULL DEFAULT NULL,
  `fkgqyyzjlr` decimal(18, 4) NULL DEFAULT NULL,
  `gx` decimal(18, 4) NULL DEFAULT NULL,
  `mggx` decimal(18, 4) NULL DEFAULT NULL,
  `mgsy` decimal(18, 4) NULL DEFAULT NULL,
  `jbmgsy` decimal(18, 4) NULL DEFAULT NULL,
  `xsmgsy` decimal(18, 4) NULL DEFAULT NULL,
  `qtqmsy` decimal(18, 4) NULL DEFAULT NULL,
  `mqsyze` decimal(18, 4) NULL DEFAULT NULL,
  `bgsyyryzqmsyze` decimal(18, 4) NULL DEFAULT NULL,
  `fkgqyyzqmsyze` decimal(18, 4) NULL DEFAULT NULL,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `enddate`(`enddate`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27886 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_hk_mainindex
-- ----------------------------
DROP TABLE IF EXISTS `em_hk_mainindex`;
CREATE TABLE `em_hk_mainindex`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `yyzsr` decimal(18, 2) NULL DEFAULT NULL,
  `gsjlr` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsrtbzz` decimal(18, 2) NULL DEFAULT NULL,
  `gsjlrtbzz` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlrtbzz` decimal(18, 2) NULL DEFAULT NULL,
  `jqjzcsyl` decimal(18, 2) NULL DEFAULT NULL,
  `tbjzcsyl` decimal(18, 2) NULL DEFAULT NULL,
  `tbzzcsyl` decimal(18, 2) NULL DEFAULT NULL,
  `mll` decimal(18, 2) NULL DEFAULT NULL,
  `jll` decimal(18, 2) NULL DEFAULT NULL,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `date` date NULL DEFAULT NULL,
  `markettype` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `date`(`date`) USING BTREE,
  INDEX `jqjzcsyl`(`jqjzcsyl`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 92842 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_hk_mainindex_common
-- ----------------------------
DROP TABLE IF EXISTS `em_hk_mainindex_common`;
CREATE TABLE `em_hk_mainindex_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `enddate` date NULL DEFAULT NULL,
  `jbmgsy` decimal(18, 4) NULL DEFAULT NULL,
  `xsmgsy` decimal(18, 4) NULL DEFAULT NULL,
  `ttmmgsy` decimal(18, 4) NULL DEFAULT NULL,
  `mgjzc` decimal(18, 4) NULL DEFAULT NULL,
  `mgjyxjl` decimal(18, 4) NULL DEFAULT NULL,
  `mgyysr` decimal(18, 4) NULL DEFAULT NULL,
  `yyzsr` decimal(18, 4) NULL DEFAULT NULL,
  `mlr` decimal(18, 4) NULL DEFAULT NULL,
  `gsjlr` decimal(18, 4) NULL DEFAULT NULL,
  `yyzsrtbzz` decimal(18, 4) NULL DEFAULT NULL,
  `mlrtbzz` decimal(18, 4) NULL DEFAULT NULL,
  `gmjlrtbzz` decimal(18, 4) NULL DEFAULT NULL,
  `yyzsrgdhbzz` decimal(18, 4) NULL DEFAULT NULL,
  `mlrgdhbzz` decimal(18, 4) NULL DEFAULT NULL,
  `gmjlrgdhbzz` decimal(18, 4) NULL DEFAULT NULL,
  `pjjzcsyl` decimal(18, 4) NULL DEFAULT NULL,
  `nhjzcsyl` decimal(18, 4) NULL DEFAULT NULL,
  `zzcjll` decimal(18, 4) NULL DEFAULT NULL,
  `mll` decimal(18, 4) NULL DEFAULT NULL,
  `jll` decimal(18, 4) NULL DEFAULT NULL,
  `nhtzhbl` decimal(18, 4) NULL DEFAULT NULL,
  `sdslrze` decimal(18, 4) NULL DEFAULT NULL,
  `jyxjlyysr` decimal(18, 4) NULL DEFAULT NULL,
  `zcfzl` decimal(18, 4) NULL DEFAULT NULL,
  `ldfzzfz` decimal(18, 4) NULL DEFAULT NULL,
  `ldbl` decimal(18, 4) NULL DEFAULT NULL,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `enddate`(`enddate`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 29703 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_inc_common_v1
-- ----------------------------
DROP TABLE IF EXISTS `em_inc_common_v1`;
CREATE TABLE `em_inc_common_v1`  (
  `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT,
  `secucode` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `security_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `report_date` date NULL DEFAULT NULL,
  `notice_date` date NULL DEFAULT NULL,
  `update_date` date NULL DEFAULT NULL,
  `total_operate_income` decimal(18, 2) NULL DEFAULT NULL,
  `operate_income` decimal(18, 2) NULL DEFAULT NULL,
  `interest_income` decimal(18, 2) NULL DEFAULT NULL,
  `earned_premium` decimal(18, 2) NULL DEFAULT NULL,
  `fee_commission_income` decimal(18, 2) NULL DEFAULT NULL,
  `other_business_income` decimal(18, 2) NULL DEFAULT NULL,
  `toi_other` decimal(18, 2) NULL DEFAULT NULL,
  `total_operate_cost` decimal(18, 2) NULL DEFAULT NULL,
  `operate_cost` decimal(18, 2) NULL DEFAULT NULL,
  `interest_expense` decimal(18, 2) NULL DEFAULT NULL,
  `fee_commission_expense` decimal(18, 2) NULL DEFAULT NULL,
  `research_expense` decimal(18, 2) NULL DEFAULT NULL,
  `surrender_value` decimal(18, 2) NULL DEFAULT NULL,
  `net_compensate_expense` decimal(18, 2) NULL DEFAULT NULL,
  `net_contract_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `policy_bonus_expense` decimal(18, 2) NULL DEFAULT NULL,
  `reinsure_expense` decimal(18, 2) NULL DEFAULT NULL,
  `other_business_cost` decimal(18, 2) NULL DEFAULT NULL,
  `operate_tax_add` decimal(18, 2) NULL DEFAULT NULL,
  `sale_expense` decimal(18, 2) NULL DEFAULT NULL,
  `manage_expense` decimal(18, 2) NULL DEFAULT NULL,
  `me_research_expense` decimal(18, 2) NULL DEFAULT NULL,
  `finance_expense` decimal(18, 2) NULL DEFAULT NULL,
  `fe_interest_expense` decimal(18, 2) NULL DEFAULT NULL,
  `fe_interest_income` decimal(18, 2) NULL DEFAULT NULL,
  `asset_impairment_loss` decimal(18, 2) NULL DEFAULT NULL,
  `credit_impairment_loss` decimal(18, 2) NULL DEFAULT NULL,
  `toc_other` decimal(18, 2) NULL DEFAULT NULL,
  `fairvalue_change_income` decimal(18, 2) NULL DEFAULT NULL,
  `invest_income` decimal(18, 2) NULL DEFAULT NULL,
  `invest_joint_income` decimal(18, 2) NULL DEFAULT NULL,
  `net_exposure_income` decimal(18, 2) NULL DEFAULT NULL,
  `exchange_income` decimal(18, 2) NULL DEFAULT NULL,
  `asset_disposal_income` decimal(18, 2) NULL DEFAULT NULL,
  `asset_impairment_income` decimal(18, 2) NULL DEFAULT NULL,
  `credit_impairment_income` decimal(18, 2) NULL DEFAULT NULL,
  `other_income` decimal(18, 2) NULL DEFAULT NULL,
  `operate_profit_other` decimal(18, 2) NULL DEFAULT NULL,
  `operate_profit_balance` decimal(18, 2) NULL DEFAULT NULL,
  `operate_profit` decimal(18, 2) NULL DEFAULT NULL,
  `nonbusiness_income` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_disposal_income` decimal(18, 2) NULL DEFAULT NULL,
  `nonbusiness_expense` decimal(18, 2) NULL DEFAULT NULL,
  `noncurrent_disposal_loss` decimal(18, 2) NULL DEFAULT NULL,
  `effect_tp_other` decimal(18, 2) NULL DEFAULT NULL,
  `total_profit_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_profit` decimal(18, 2) NULL DEFAULT NULL,
  `income_tax` decimal(18, 2) NULL DEFAULT NULL,
  `effect_netprofit_other` decimal(18, 2) NULL DEFAULT NULL,
  `effect_netprofit_balance` decimal(18, 2) NULL DEFAULT NULL,
  `unconfirm_invest_loss` decimal(18, 2) NULL DEFAULT NULL,
  `netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `precombine_profit` decimal(18, 2) NULL DEFAULT NULL,
  `continued_netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `discontinued_netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `parent_netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `minority_interest` decimal(18, 2) NULL DEFAULT NULL,
  `deduct_parent_netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `netprofit_other` decimal(18, 2) NULL DEFAULT NULL,
  `netprofit_balance` decimal(18, 2) NULL DEFAULT NULL,
  `basic_eps` decimal(18, 2) NULL DEFAULT NULL,
  `diluted_eps` decimal(18, 2) NULL DEFAULT NULL,
  `other_compre_income` decimal(18, 2) NULL DEFAULT NULL,
  `parent_oci` decimal(18, 2) NULL DEFAULT NULL,
  `minority_oci` decimal(18, 2) NULL DEFAULT NULL,
  `parent_oci_other` decimal(18, 2) NULL DEFAULT NULL,
  `parent_oci_balance` decimal(18, 2) NULL DEFAULT NULL,
  `unable_oci` decimal(18, 2) NULL DEFAULT NULL,
  `creditrisk_fairvalue_change` decimal(18, 2) NULL DEFAULT NULL,
  `otherright_fairvalue_change` decimal(18, 2) NULL DEFAULT NULL,
  `setup_profit_change` decimal(18, 2) NULL DEFAULT NULL,
  `rightlaw_unable_oci` decimal(18, 2) NULL DEFAULT NULL,
  `unable_oci_other` decimal(18, 2) NULL DEFAULT NULL,
  `unable_oci_balance` decimal(18, 2) NULL DEFAULT NULL,
  `able_oci` decimal(18, 2) NULL DEFAULT NULL,
  `rightlaw_able_oci` decimal(18, 2) NULL DEFAULT NULL,
  `afa_fairvalue_change` decimal(18, 2) NULL DEFAULT NULL,
  `hmi_afa` decimal(18, 2) NULL DEFAULT NULL,
  `cashflow_hedge_valid` decimal(18, 2) NULL DEFAULT NULL,
  `creditor_fairvalue_change` decimal(18, 2) NULL DEFAULT NULL,
  `creditor_impairment_reserve` decimal(18, 2) NULL DEFAULT NULL,
  `finance_oci_amt` decimal(18, 2) NULL DEFAULT NULL,
  `convert_diff` decimal(18, 2) NULL DEFAULT NULL,
  `able_oci_other` decimal(18, 2) NULL DEFAULT NULL,
  `able_oci_balance` decimal(18, 2) NULL DEFAULT NULL,
  `oci_other` decimal(18, 2) NULL DEFAULT NULL,
  `oci_balance` decimal(18, 2) NULL DEFAULT NULL,
  `total_compre_income` decimal(18, 2) NULL DEFAULT NULL,
  `parent_tci` decimal(18, 2) NULL DEFAULT NULL,
  `minority_tci` decimal(18, 2) NULL DEFAULT NULL,
  `precombine_tci` decimal(18, 2) NULL DEFAULT NULL,
  `effect_tci_balance` decimal(18, 2) NULL DEFAULT NULL,
  `tci_other` decimal(18, 2) NULL DEFAULT NULL,
  `tci_balance` decimal(18, 2) NULL DEFAULT NULL,
  `acf_end_income` decimal(18, 2) NULL DEFAULT NULL,
  `opinion_type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `secucode`(`secucode`) USING BTREE,
  INDEX `report_date`(`report_date`) USING BTREE,
  INDEX `secucode_2`(`secucode`, `report_date`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 219344 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_income_bank
-- ----------------------------
DROP TABLE IF EXISTS `em_income_bank`;
CREATE TABLE `em_income_bank`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `OPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `INTNREVE` decimal(18, 2) NULL DEFAULT NULL,
  `INTREVE` decimal(18, 2) NULL DEFAULT NULL,
  `INTEXP` decimal(18, 2) NULL DEFAULT NULL,
  `COMMNREVE` decimal(18, 2) NULL DEFAULT NULL,
  `COMMREVE` decimal(18, 2) NULL DEFAULT NULL,
  `COMMEXP` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTJOINTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `EXCHANGEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREVE` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATETAX` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEMANAGEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `ASSETDEVALUELOSS` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `INCOMETAX` decimal(18, 2) NULL DEFAULT NULL,
  `NETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTNETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `KCFJCXSYJLR` decimal(18, 2) NULL DEFAULT NULL,
  `DILUTEDEPS` decimal(18, 2) NULL DEFAULT NULL,
  `BASICEPS` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `SUMCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1733 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_income_broker
-- ----------------------------
DROP TABLE IF EXISTS `em_income_broker`;
CREATE TABLE `em_income_broker`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `OPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `COMMNREVE` decimal(18, 2) NULL DEFAULT NULL,
  `AGENTTRADESECURITY` decimal(18, 2) NULL DEFAULT NULL,
  `SECURITYUW` decimal(18, 2) NULL DEFAULT NULL,
  `CLIENTASSETMANAGE` decimal(18, 2) NULL DEFAULT NULL,
  `FINACONSULT` decimal(18, 2) NULL DEFAULT NULL,
  `SPONSOR` decimal(18, 2) NULL DEFAULT NULL,
  `FUNDMANAGE` decimal(18, 2) NULL DEFAULT NULL,
  `FUNDSALE` decimal(18, 2) NULL DEFAULT NULL,
  `SECURITYBROKER` decimal(18, 2) NULL DEFAULT NULL,
  `COMMNREVEOTHER` decimal(18, 2) NULL DEFAULT NULL,
  `INTNREVE` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTJOINTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEOSALABLE` decimal(18, 2) NULL DEFAULT NULL,
  `EXCHANGEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREVE` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATETAX` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEMANAGEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `ASSETDEVALUELOSS` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETNETLOSS` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `INCOMETAX` decimal(18, 2) NULL DEFAULT NULL,
  `NETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTNETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `KCFJCXSYJLR` decimal(18, 2) NULL DEFAULT NULL,
  `BASICEPS` decimal(18, 2) NULL DEFAULT NULL,
  `DILUTEDEPS` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `SUMCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1982 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_income_common
-- ----------------------------
DROP TABLE IF EXISTS `em_income_common`;
CREATE TABLE `em_income_common`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `TOTALOPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `INTREVE` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMEARNED` decimal(18, 2) NULL DEFAULT NULL,
  `COMMREVE` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREVE` decimal(18, 2) NULL DEFAULT NULL,
  `TOTALOPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `INTEXP` decimal(18, 2) NULL DEFAULT NULL,
  `COMMEXP` decimal(18, 2) NULL DEFAULT NULL,
  `RDEXP` decimal(18, 2) NULL DEFAULT NULL,
  `SURRENDERPREMIUM` decimal(18, 2) NULL DEFAULT NULL,
  `NETINDEMNITYEXP` decimal(18, 2) NULL DEFAULT NULL,
  `NETCONTACTRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `POLICYDIVIEXP` decimal(18, 2) NULL DEFAULT NULL,
  `RIEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATETAX` decimal(18, 2) NULL DEFAULT NULL,
  `SALEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `MANAGEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `FINANCEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `ASSETDEVALUELOSS` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTJOINTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `EXCHANGEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETNETLOSS` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `INCOMETAX` decimal(18, 2) NULL DEFAULT NULL,
  `NETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `COMBINEDNETPROFITB` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTNETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `KCFJCXSYJLR` decimal(18, 2) NULL DEFAULT NULL,
  `BASICEPS` decimal(18, 2) NULL DEFAULT NULL,
  `DILUTEDEPS` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `SUMCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `operatereve_p` decimal(18, 2) NULL DEFAULT NULL,
  `operateexp_p` decimal(18, 2) NULL DEFAULT NULL,
  `rdexp_p` decimal(18, 2) NULL DEFAULT NULL,
  `operatetax_p` decimal(18, 2) NULL DEFAULT NULL,
  `saleexp_p` decimal(18, 2) NULL DEFAULT NULL,
  `manageexp_p` decimal(18, 2) NULL DEFAULT NULL,
  `financeexp_p` decimal(18, 2) NULL DEFAULT NULL,
  `assetdevalueloss_p` decimal(18, 2) NULL DEFAULT NULL,
  `fvalueincome_p` decimal(18, 2) NULL DEFAULT NULL,
  `investincome_p` decimal(18, 2) NULL DEFAULT NULL,
  `operateprofit_p` decimal(18, 2) NULL DEFAULT NULL,
  `nonoperatereve_p` decimal(18, 2) NULL DEFAULT NULL,
  `nonoperateexp_p` decimal(18, 2) NULL DEFAULT NULL,
  `sumprofit_p` decimal(18, 2) NULL DEFAULT NULL,
  `incometax_p` decimal(18, 2) NULL DEFAULT NULL,
  `netprofit_p` decimal(18, 2) NULL DEFAULT NULL,
  `parentnetprofit_p` decimal(18, 2) NULL DEFAULT NULL,
  `minorityincome_p` decimal(18, 2) NULL DEFAULT NULL,
  `kcfjcxsyjlr_p` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr_zs` decimal(18, 2) NULL DEFAULT NULL,
  `main_revenue` decimal(18, 2) NULL DEFAULT NULL,
  `main_biz_sale` decimal(18, 2) NULL DEFAULT NULL,
  `interestexp` decimal(18, 2) NULL DEFAULT NULL,
  `sales_eq` decimal(18, 2) NULL DEFAULT NULL,
  `fore_netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `sales` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_KCFJCXSYJLR`(`KCFJCXSYJLR`) USING BTREE,
  INDEX `kfjlr_zs`(`kfjlr_zs`) USING BTREE,
  INDEX `SECURITYCODE`(`SECURITYCODE`) USING BTREE,
  INDEX `REPORTDATE`(`REPORTDATE`) USING BTREE,
  INDEX `SECURITYCODE_2`(`SECURITYCODE`, `REPORTDATE`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 205069 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_income_insurance
-- ----------------------------
DROP TABLE IF EXISTS `em_income_insurance`;
CREATE TABLE `em_income_insurance`  (
  `id` bigint(18) NOT NULL AUTO_INCREMENT,
  `SECURITYCODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `REPORTTYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `TYPE` smallint(5) UNSIGNED NULL DEFAULT NULL,
  `REPORTDATE` date NULL DEFAULT NULL,
  `OPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `PREMIUMEARNED` decimal(18, 2) NULL DEFAULT NULL,
  `INSURREVE` decimal(18, 2) NULL DEFAULT NULL,
  `RIREVE` decimal(18, 2) NULL DEFAULT NULL,
  `RIPREMIUM` decimal(18, 2) NULL DEFAULT NULL,
  `UNDUERESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `BANKINTNREVE` decimal(18, 2) NULL DEFAULT NULL,
  `BANKINTREVE` decimal(18, 2) NULL DEFAULT NULL,
  `BANKINTEXP` decimal(18, 2) NULL DEFAULT NULL,
  `NINSURCOMMNREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NINSURCOMMREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NINSURCOMMEXP` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `INVESTJOINTINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `FVALUEOSALABLE` decimal(18, 2) NULL DEFAULT NULL,
  `EXCHANGEINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERREVE` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `SURRENDERPREMIUM` decimal(18, 2) NULL DEFAULT NULL,
  `INDEMNITYEXP` decimal(18, 2) NULL DEFAULT NULL,
  `AMORTISEINDEMNITYEXP` decimal(18, 2) NULL DEFAULT NULL,
  `DUTYRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `AMORTISEDUTYRESERVE` decimal(18, 2) NULL DEFAULT NULL,
  `POLICYDIVIEXP` decimal(18, 2) NULL DEFAULT NULL,
  `RIEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATETAX` decimal(18, 2) NULL DEFAULT NULL,
  `COMMEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEMANAGEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `AMORTISERIEXP` decimal(18, 2) NULL DEFAULT NULL,
  `INTEXP` decimal(18, 2) NULL DEFAULT NULL,
  `FINANCEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `OTHEREXP` decimal(18, 2) NULL DEFAULT NULL,
  `ASSETDEVALUELOSS` decimal(18, 2) NULL DEFAULT NULL,
  `OPERATEPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETREVE` decimal(18, 2) NULL DEFAULT NULL,
  `NONOPERATEEXP` decimal(18, 2) NULL DEFAULT NULL,
  `NONLASSETNETLOSS` decimal(18, 2) NULL DEFAULT NULL,
  `SUMPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `INCOMETAX` decimal(18, 2) NULL DEFAULT NULL,
  `NETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTNETPROFIT` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `KCFJCXSYJLR` decimal(18, 2) NULL DEFAULT NULL,
  `BASICEPS` decimal(18, 2) NULL DEFAULT NULL,
  `DILUTEDEPS` decimal(18, 2) NULL DEFAULT NULL,
  `OTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYOTHERCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `SUMCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `PARENTCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  `MINORITYCINCOME` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 294 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for em_major_business_scope
-- ----------------------------
DROP TABLE IF EXISTS `em_major_business_scope`;
CREATE TABLE `em_major_business_scope`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zygc` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `zysr` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `srbl` decimal(18, 2) NULL DEFAULT NULL,
  `zycb` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `cbbl` decimal(18, 2) NULL DEFAULT NULL,
  `zylr` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `lrbl` decimal(18, 2) NULL DEFAULT NULL,
  `mll` decimal(18, 2) NULL DEFAULT NULL,
  `date` date NULL DEFAULT NULL,
  `ts_code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `type` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `color` int(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `date`(`date`) USING BTREE,
  INDEX `type`(`type`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 178423 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ent_compr_analysis
-- ----------------------------
DROP TABLE IF EXISTS `ent_compr_analysis`;
CREATE TABLE `ent_compr_analysis`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `cate_name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `sort` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 312 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fore_np
-- ----------------------------
DROP TABLE IF EXISTS `fore_np`;
CREATE TABLE `fore_np`  (
  `ts_code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `fore_np` decimal(18, 2) NULL DEFAULT NULL,
  `fore_type` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pre_np` decimal(18, 2) NULL DEFAULT NULL,
  `for_np_chg` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `reason` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `summary` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `name`(`name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fore_np2021h1
-- ----------------------------
DROP TABLE IF EXISTS `fore_np2021h1`;
CREATE TABLE `fore_np2021h1`  (
  `ts_code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `fore_np` decimal(18, 2) NULL DEFAULT NULL,
  `fore_type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for foreign_debt
-- ----------------------------
DROP TABLE IF EXISTS `foreign_debt`;
CREATE TABLE `foreign_debt`  (
  `date` datetime(0) NULL DEFAULT NULL,
  `name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `code` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `guimo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `jiaogeri` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `daoqiri` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pingji` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `xipiaolv` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `shouyilv` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `title` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `faxingren` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `text` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for gqzy
-- ----------------------------
DROP TABLE IF EXISTS `gqzy`;
CREATE TABLE `gqzy`  (
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `zybs` decimal(18, 2) NULL DEFAULT NULL,
  `wxszysl` decimal(18, 2) NULL DEFAULT NULL,
  `yxszysl` decimal(18, 2) NULL DEFAULT NULL,
  `a_totalnums` decimal(18, 2) NULL DEFAULT NULL,
  `zy_ratio` decimal(18, 2) NULL DEFAULT NULL,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for measuring_the_moat
-- ----------------------------
DROP TABLE IF EXISTS `measuring_the_moat`;
CREATE TABLE `measuring_the_moat`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `competitive_life_cycle_stage` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `is_return_above_cost` tinyint(1) NULL DEFAULT NULL,
  `roic_status` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roic_status_remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `invest_spending_trend` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `each_player_represent_percent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `each_player_profit_level` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `market_share_historical_trends` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `market_share_how_stable` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `pricing_trends` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `industry_class` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `suppliers_leverage` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `pass_supplier_incto_customers` tinyint(1) NULL DEFAULT NULL,
  `substi_prod_available` tinyint(1) NULL DEFAULT NULL,
  `have_switching_costs` tinyint(1) NULL DEFAULT NULL,
  `buyers_leverage` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `buyers_informed` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `entry_and_exit_rates` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `reactions_newentrants` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `reputation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `asset_specificity` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `mini_effic_prod_scale` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `excess_capacity` tinyint(1) NULL DEFAULT NULL,
  `way_to_diff_prod` tinyint(1) NULL DEFAULT NULL,
  `antici_payoff_for_new_entrant` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `precommit_contracts` tinyint(1) NULL DEFAULT NULL,
  `licenses_or_patents` tinyint(1) NULL DEFAULT NULL,
  `learning_curve_benefits` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `pricing_coordination` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `industry_concentration` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `size_distribution` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `incent_corp_ownership_structure` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `demand_variability` tinyint(1) NULL DEFAULT NULL,
  `high_fixed_costs` tinyint(1) NULL DEFAULT NULL,
  `industry_growing` tinyint(1) NULL DEFAULT NULL,
  `vulnerable_disruptive_innovation` tinyint(1) NULL DEFAULT NULL,
  `innovations_foster_prod_improve` tinyint(1) NULL DEFAULT NULL,
  `inno_progre_faster_market_needs` tinyint(1) NULL DEFAULT NULL,
  `estab_players_passed_perform_threshold` tinyint(1) NULL DEFAULT NULL,
  `organized_type` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `value_chain_diff_rivals` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `production_advantages` tinyint(1) NULL DEFAULT NULL,
  `biz_stuct_instable` tinyint(1) NULL DEFAULT NULL,
  `complex_co_cap` tinyint(1) NULL DEFAULT NULL,
  `how_quick_process_costs_chg` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `patents_copyrights` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `economies_scale` tinyint(1) NULL DEFAULT NULL,
  `distribution_scale` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `ass_reve_clustered_geo` tinyint(1) NULL DEFAULT NULL,
  `purchasing_advantages` tinyint(1) NULL DEFAULT NULL,
  `economies_scope` tinyint(1) NULL DEFAULT NULL,
  `diverse_research_profiles` tinyint(1) NULL DEFAULT NULL,
  `consumer_advantages` tinyint(1) NULL DEFAULT NULL,
  `habit_horizontal_diff` tinyint(1) NULL DEFAULT NULL,
  `prefer_pro_or_competing_pro` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `customers_weigh_lots_pro_attrs` tinyint(1) NULL DEFAULT NULL,
  `customers_through_trial` tinyint(1) NULL DEFAULT NULL,
  `customer_lockin_switching_costs` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `network_radial_or_interactive` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `source_longevity_added_value` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `external_sources_added_value` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `industry_include_complementors` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `value_pie_growing` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `hire_brand_for_job` tinyint(1) NULL DEFAULT NULL,
  `brand_increase_will_to_pay` tinyint(1) NULL DEFAULT NULL,
  `emotional_conn_brand` tinyint(1) NULL DEFAULT NULL,
  `trust_pro_cause_name` tinyint(1) NULL DEFAULT NULL,
  `brand_imply_social_status` tinyint(1) NULL DEFAULT NULL,
  `reduce_supplier_opercost_with_name` tinyint(1) NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for quant2017
-- ----------------------------
DROP TABLE IF EXISTS `quant2017`;
CREATE TABLE `quant2017`  (
  `日期` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `行业` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `名称` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `上市时间` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `市值` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PB` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `财务杠杆` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ROE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `毛利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `净利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `营收增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利润总额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `资产负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `金融资产` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `货币资金` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `短期债务` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `经营现金流净额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `扣非归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出及分红等` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `当年分红` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `子公司支付给少数股东的股利及利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for quant2018
-- ----------------------------
DROP TABLE IF EXISTS `quant2018`;
CREATE TABLE `quant2018`  (
  `日期` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `行业` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `名称` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `上市时间` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `市值` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PB` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `财务杠杆` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ROE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `毛利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `净利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `营收增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利润总额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `资产负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `金融资产` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `货币资金` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `短期债务` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `经营现金流净额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `扣非归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出及分红等` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `当年分红` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `子公司支付给少数股东的股利及利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for quant2019
-- ----------------------------
DROP TABLE IF EXISTS `quant2019`;
CREATE TABLE `quant2019`  (
  `日期` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `行业` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `名称` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `上市时间` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `市值` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PB` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `财务杠杆` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ROE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `毛利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `净利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `营收增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利润总额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `资产负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `金融资产` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `货币资金` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `短期债务` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `经营现金流净额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `扣非归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出及分红等` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `当年分红` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `子公司支付给少数股东的股利及利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for quant2020
-- ----------------------------
DROP TABLE IF EXISTS `quant2020`;
CREATE TABLE `quant2020`  (
  `日期` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `行业` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `名称` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `上市时间` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `市值` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PB` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `财务杠杆` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ROE` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `毛利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `净利率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `营收增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润增速` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利润总额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `资产负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债率` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `金融资产` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `货币资金` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `有息负债` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `短期债务` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `经营现金流净额` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `扣非归母净利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出及分红等` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `当年分红` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `子公司支付给少数股东的股利及利润` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `利息支出` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for roelt10in3years
-- ----------------------------
DROP TABLE IF EXISTS `roelt10in3years`;
CREATE TABLE `roelt10in3years`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sina_bond
-- ----------------------------
DROP TABLE IF EXISTS `sina_bond`;
CREATE TABLE `sina_bond`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `trade` decimal(18, 2) NULL DEFAULT NULL,
  `changepercent` decimal(18, 2) NULL DEFAULT NULL,
  `settlement` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8598 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for static_chginyear
-- ----------------------------
DROP TABLE IF EXISTS `static_chginyear`;
CREATE TABLE `static_chginyear`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `chg` decimal(18, 2) NULL DEFAULT NULL,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_chg_tscode`(`ts_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3887 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for static_cons_inv_roe
-- ----------------------------
DROP TABLE IF EXISTS `static_cons_inv_roe`;
CREATE TABLE `static_cons_inv_roe`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `mc` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit2019` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_margin2019` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit202006` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_margin202006` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit2018` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_margin2018` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit2017` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_margin2017` decimal(18, 2) NULL DEFAULT NULL,
  `const2017` decimal(18, 2) NULL DEFAULT NULL,
  `const2018` decimal(18, 2) NULL DEFAULT NULL,
  `const2019` decimal(18, 2) NULL DEFAULT NULL,
  `const202006` decimal(18, 2) NULL DEFAULT NULL,
  `const_prop` decimal(18, 2) NULL DEFAULT NULL,
  `const_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `inv2017` decimal(18, 2) NULL DEFAULT NULL,
  `inv2018` decimal(18, 2) NULL DEFAULT NULL,
  `inv2019` decimal(18, 2) NULL DEFAULT NULL,
  `inv202006` decimal(18, 2) NULL DEFAULT NULL,
  `inv_prop` decimal(18, 2) NULL DEFAULT NULL,
  `inv_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `roe2017` decimal(18, 2) NULL DEFAULT NULL,
  `roe2018` decimal(18, 2) NULL DEFAULT NULL,
  `roe2019` decimal(18, 2) NULL DEFAULT NULL,
  `roe202006` decimal(18, 2) NULL DEFAULT NULL,
  `roe202009` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsr2017` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsr2018` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsr2019` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsr202006` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsrtbzz2017` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsrtbzz2018` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsrtbzz2019` decimal(18, 2) NULL DEFAULT NULL,
  `yyzsrtbzz202006` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr2017` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr2018` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr2019` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr202006` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlrtbzz2017` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlrtbzz2018` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlrtbzz2019` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlrtbzz202006` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_hxl2017` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_hxl2018` decimal(18, 2) NULL DEFAULT NULL,
  `core_profit_hxl2019` decimal(18, 2) NULL DEFAULT NULL,
  `rdspendsum` decimal(18, 2) NULL DEFAULT NULL,
  `rdspendsum_ratio` decimal(18, 2) NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `rdspendsum`(`rdspendsum`) USING BTREE,
  INDEX `rdspendsum_ratio`(`rdspendsum_ratio`) USING BTREE,
  INDEX `roe2019`(`roe2019`) USING BTREE,
  INDEX `roe2018`(`roe2018`) USING BTREE,
  INDEX `roe202009`(`roe202009`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for static_mainindex
-- ----------------------------
DROP TABLE IF EXISTS `static_mainindex`;
CREATE TABLE `static_mainindex`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roe2017` decimal(18, 2) NULL DEFAULT NULL,
  `roe2018` decimal(18, 2) NULL DEFAULT NULL,
  `roe2019` decimal(18, 2) NULL DEFAULT NULL,
  `roe2020` decimal(18, 2) NULL DEFAULT NULL,
  `rdspendsum` decimal(18, 2) NULL DEFAULT NULL,
  `rdspendsum_ratio` decimal(18, 2) NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `roe2018`(`roe2018`) USING BTREE,
  INDEX `roe2019`(`roe2019`) USING BTREE,
  INDEX `roe2020`(`roe2020`) USING BTREE,
  INDEX `rdspendsum`(`rdspendsum`) USING BTREE,
  INDEX `rdspendsum_ratio`(`rdspendsum_ratio`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for static_roe
-- ----------------------------
DROP TABLE IF EXISTS `static_roe`;
CREATE TABLE `static_roe`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `list_year` int(11) NULL DEFAULT NULL,
  `net_oper_cashflow_in5years` int(11) NULL DEFAULT NULL,
  `net_fin_cashflow_in5years` int(11) NULL DEFAULT NULL,
  `roe_great10_years` int(11) NULL DEFAULT NULL,
  `roe_great15_years` int(11) NULL DEFAULT NULL,
  `roe_great10_in5years` int(11) NULL DEFAULT NULL,
  `roe_great15_in5years` int(11) NULL DEFAULT NULL,
  `fin_cashflow_gt0_years` int(11) NULL DEFAULT NULL,
  `oper_cashflow_gt0_in5years` int(11) NULL DEFAULT NULL,
  `fin_cashflow_gt0_in5years` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `oper_cashflow_gt_np_in5years` int(11) NULL DEFAULT NULL,
  `zys_cagr_10years` decimal(18, 2) NULL DEFAULT NULL,
  `jlr_cagr_10years` decimal(18, 2) NULL DEFAULT NULL,
  `fcf_cagr_10years` decimal(18, 2) NULL DEFAULT NULL,
  `net_oper_cashflow_in10years` decimal(18, 2) NULL DEFAULT NULL,
  `net_fin_cashflow_in10years` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_roe_tsoce`(`ts_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3611 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for static_value_estimate
-- ----------------------------
DROP TABLE IF EXISTS `static_value_estimate`;
CREATE TABLE `static_value_estimate`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sshy` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `mc` decimal(18, 2) NULL DEFAULT NULL,
  `netopcf` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr` decimal(18, 2) NULL DEFAULT NULL,
  `rdsum` decimal(18, 2) NULL DEFAULT NULL,
  INDEX `ts_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_basic
-- ----------------------------
DROP TABLE IF EXISTS `stock_basic`;
CREATE TABLE `stock_basic`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `area` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `industry` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `market` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `list_date` date NULL DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `market_capital` decimal(18, 2) NULL DEFAULT NULL,
  `dividend` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_yield` decimal(18, 2) NULL DEFAULT NULL,
  `float_market_capital` decimal(18, 2) NULL DEFAULT NULL,
  `high52w` decimal(18, 2) NULL DEFAULT NULL,
  `low52w` decimal(18, 2) NULL DEFAULT NULL,
  `navps` decimal(18, 2) NULL DEFAULT NULL,
  `eps` decimal(18, 2) NULL DEFAULT NULL,
  `pb` decimal(18, 2) NULL DEFAULT NULL,
  `pe_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `total_dividends` int(11) NULL DEFAULT NULL,
  `sum_dividends` decimal(18, 2) NULL DEFAULT NULL,
  `total_ipo` int(11) NULL DEFAULT NULL,
  `sum_ipo` decimal(18, 2) NULL DEFAULT NULL,
  `total_pp` int(11) NULL DEFAULT NULL,
  `sum_pp` decimal(18, 2) NULL DEFAULT NULL,
  `total_ri` int(11) NULL DEFAULT NULL,
  `sum_ri` decimal(18, 2) NULL DEFAULT NULL,
  `name_py` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `is_spec` tinyint(1) NULL DEFAULT NULL,
  `ths_fhze` decimal(18, 2) NULL DEFAULT NULL,
  `ths_rzhj` decimal(18, 2) NULL DEFAULT NULL,
  `ths_ljfhcs` int(11) NULL DEFAULT NULL,
  `ths_rdspendsum` decimal(18, 2) NULL DEFAULT NULL,
  `ths_rdspendsum_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `ths_rdinvest` decimal(18, 2) NULL DEFAULT NULL,
  `ths_rdinvest_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `xq_industry` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ths_staffs_num` int(18) NULL DEFAULT NULL,
  `interest_exchange` decimal(18, 2) NULL DEFAULT NULL,
  `interest_income` decimal(18, 2) NULL DEFAULT NULL,
  `kggdcgbl` decimal(18, 2) NULL DEFAULT NULL,
  `kggdzyzzgb` decimal(18, 2) NULL DEFAULT NULL,
  `gsczrljzybl` decimal(18, 2) NULL DEFAULT NULL,
  `sjkgr_cgbl` decimal(18, 2) NULL DEFAULT NULL,
  `sjkgr` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `fore_np` decimal(18, 2) NULL DEFAULT NULL,
  `fore_type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `fore_chg` decimal(18, 2) NULL DEFAULT NULL,
  `fore_reason` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `fore_summary` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `mll_avg` decimal(18, 2) NULL DEFAULT NULL,
  `mll_fc` decimal(18, 2) NULL DEFAULT NULL,
  `net_oper_cf_avg` decimal(18, 2) NULL DEFAULT NULL,
  `net_oper_cf_gt0_count` int(18) NULL DEFAULT NULL,
  `net_fin_cf_gt0_count` int(18) NULL DEFAULT NULL,
  `roe_gt10_count` int(11) NULL DEFAULT NULL,
  `roe_gt15_count` int(11) NULL DEFAULT NULL,
  `current` decimal(18, 2) NULL DEFAULT NULL,
  `percent` decimal(18, 2) NULL DEFAULT NULL,
  `pledge_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `goodwill_in_net_assets` decimal(18, 2) NULL DEFAULT NULL,
  `turnover_rate` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ix_stock_basic_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3896 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_company
-- ----------------------------
DROP TABLE IF EXISTS `stock_company`;
CREATE TABLE `stock_company`  (
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `exchange` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `chairman` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `manager` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `secretary` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `reg_capital` double NULL DEFAULT NULL,
  `setup_date` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `province` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `city` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `website` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `email` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `employees` double NULL DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3702 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_us
-- ----------------------------
DROP TABLE IF EXISTS `stock_us`;
CREATE TABLE `stock_us`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pb_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `net_profit_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `ps` decimal(18, 2) NULL DEFAULT NULL,
  `current_year_percent` decimal(18, 2) NULL DEFAULT NULL,
  `market_capital` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_yield` decimal(18, 2) NULL DEFAULT NULL,
  `roe_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `income_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `pb` decimal(18, 2) NULL DEFAULT NULL,
  `followers` decimal(18, 2) NULL DEFAULT NULL,
  `name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pe_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `main_net_inflows` decimal(18, 2) NULL DEFAULT NULL,
  `platename` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `industryname` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8001 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_us_temp
-- ----------------------------
DROP TABLE IF EXISTS `stock_us_temp`;
CREATE TABLE `stock_us_temp`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pb_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `net_profit_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `ps` decimal(18, 2) NULL DEFAULT NULL,
  `current_year_percent` decimal(18, 2) NULL DEFAULT NULL,
  `market_capital` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_yield` decimal(18, 2) NULL DEFAULT NULL,
  `roe_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `income_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `pb` decimal(18, 2) NULL DEFAULT NULL,
  `followers` decimal(18, 2) NULL DEFAULT NULL,
  `name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pe_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `main_net_inflows` decimal(18, 2) NULL DEFAULT NULL,
  `platename` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `industryname` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 40719 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ths_bonus
-- ----------------------------
DROP TABLE IF EXISTS `ths_bonus`;
CREATE TABLE `ths_bonus`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bonus` decimal(18, 2) NULL DEFAULT NULL,
  `kfjlr` decimal(18, 2) NULL DEFAULT NULL,
  `fjcxsy` decimal(18, 2) NULL DEFAULT NULL,
  `netprofit` decimal(18, 2) NULL DEFAULT NULL,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3611 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ths_hd
-- ----------------------------
DROP TABLE IF EXISTS `ths_hd`;
CREATE TABLE `ths_hd`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rdspendsum` decimal(18, 2) NULL DEFAULT NULL,
  `rdspendsum_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `rdinvest` decimal(18, 2) NULL DEFAULT NULL,
  `rdinvest_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `staffs_num` decimal(18, 2) NULL DEFAULT NULL,
  `reportdate` date NULL DEFAULT NULL,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `reportdate`(`reportdate`) USING BTREE,
  INDEX `reportdate_2`(`reportdate`, `ts_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 41047 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for xq_hk_stock
-- ----------------------------
DROP TABLE IF EXISTS `xq_hk_stock`;
CREATE TABLE `xq_hk_stock`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `amount` bigint(20) NULL DEFAULT NULL,
  `chg` decimal(18, 2) NULL DEFAULT NULL,
  `type` int(11) NULL DEFAULT NULL,
  `percent` decimal(18, 2) NULL DEFAULT NULL,
  `has_follow` tinyint(1) NULL DEFAULT NULL,
  `tick_size` decimal(18, 2) NULL DEFAULT NULL,
  `volume` bigint(20) NULL DEFAULT NULL,
  `current` decimal(18, 2) NULL DEFAULT NULL,
  `volume_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `amplitude` decimal(18, 2) NULL DEFAULT NULL,
  `pb` decimal(18, 2) NULL DEFAULT NULL,
  `current_year_percent` decimal(18, 2) NULL DEFAULT NULL,
  `turnover_rate` decimal(18, 2) NULL DEFAULT NULL,
  `float_market_capital` bigint(20) NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `market_capital` bigint(20) NULL DEFAULT NULL,
  `pe_ttm` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_yield` decimal(18, 2) NULL DEFAULT NULL,
  `lot_size` int(11) NULL DEFAULT NULL,
  `percent5m` decimal(18, 2) NULL DEFAULT NULL,
  `name_py` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `is_spec` tinyint(1) NULL DEFAULT NULL,
  `industry` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2598 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for xq_stock
-- ----------------------------
DROP TABLE IF EXISTS `xq_stock`;
CREATE TABLE `xq_stock`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ts_code` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `net_profit_cagr` decimal(18, 2) NULL DEFAULT NULL,
  `ps` decimal(18, 4) NULL DEFAULT NULL,
  `type` int(11) NULL DEFAULT NULL,
  `percent` decimal(18, 2) NULL DEFAULT NULL,
  `has_follow` tinyint(1) NULL DEFAULT NULL,
  `float_shares` bigint(20) NULL DEFAULT NULL,
  `current` decimal(18, 2) NULL DEFAULT NULL,
  `amplitude` decimal(18, 2) NULL DEFAULT NULL,
  `pcf` decimal(18, 4) NULL DEFAULT NULL,
  `current_year_percent` decimal(18, 2) NULL DEFAULT NULL,
  `float_market_capital` bigint(20) NULL DEFAULT NULL,
  `market_capital` bigint(20) NULL DEFAULT NULL,
  `dividend_yield` decimal(18, 2) NULL DEFAULT NULL,
  `roe_ttm` decimal(18, 13) NULL DEFAULT NULL,
  `total_percent` decimal(18, 2) NULL DEFAULT NULL,
  `percent5m` decimal(18, 2) NULL DEFAULT NULL,
  `income_cagr` decimal(18, 13) NULL DEFAULT NULL,
  `amount` decimal(18, 2) NULL DEFAULT NULL,
  `chg` decimal(18, 2) NULL DEFAULT NULL,
  `issue_date` date NULL DEFAULT NULL,
  `main_net_inflows` decimal(18, 2) NULL DEFAULT NULL,
  `volume` int(11) NULL DEFAULT NULL,
  `volume_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `pb` decimal(18, 3) NULL DEFAULT NULL,
  `followers` int(11) NULL DEFAULT NULL,
  `turnover_rate` decimal(18, 2) NULL DEFAULT NULL,
  `first_percent` decimal(18, 2) NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pinyin` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `fullpinyin` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `pe_ttm` decimal(18, 3) NULL DEFAULT NULL,
  `total_shares` bigint(20) NULL DEFAULT NULL,
  `tdxindustry` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `zjhindustry` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sshy` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sszjhhy` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `lssws` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `kjssws` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dm` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dsz` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `gsjj` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `jyfw` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `gyrs` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `glryrs` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sshy_py` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sshy_fullpy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `markettype` int(11) NULL DEFAULT NULL,
  `tradetype` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sdgd` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `sdltgd` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `is_follow` tinyint(1) NULL DEFAULT NULL,
  `fore_roe_1yearlater` decimal(18, 2) NULL DEFAULT NULL,
  `fore_roe_2yearlater` decimal(18, 2) NULL DEFAULT NULL,
  `fore_roe_3yearlater` decimal(18, 2) NULL DEFAULT NULL,
  `fore_np_1yearlater` decimal(18, 2) NULL DEFAULT NULL,
  `fore_np_2yearlater` decimal(18, 2) NULL DEFAULT NULL,
  `fore_np_3yearlater` decimal(18, 2) NULL DEFAULT NULL,
  `bm_customer_segmentation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_customer_relationship` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_channel_pathway` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_value_proposition` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_key_activities` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_key_resources` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_key_partnerships` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_cost` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `bm_revenue` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `peg` decimal(18, 2) NULL DEFAULT NULL,
  `np_growth` decimal(18, 2) NULL DEFAULT NULL,
  `lugutong_zb` decimal(18, 2) NULL DEFAULT NULL,
  `lugutong_data` decimal(18, 2) NULL DEFAULT NULL,
  `latest_np_yoy` decimal(18, 2) NULL DEFAULT NULL,
  `latest_bonus` decimal(18, 2) NULL DEFAULT NULL,
  `latest_bonus_prop` decimal(18, 2) NULL DEFAULT NULL,
  `dividend_yield_ratio` decimal(18, 2) NULL DEFAULT NULL,
  `primary_industry` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `secondary_industry` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `board_name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `board_name_pinyin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `board_name_fullpinyin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `markedas` int(2) NULL DEFAULT NULL,
  `chg720` decimal(18, 2) NULL DEFAULT NULL,
  `chg360` decimal(18, 2) NULL DEFAULT NULL,
  `pb4yearsago` decimal(18, 2) NULL DEFAULT NULL,
  `pb1yearsago` decimal(18, 2) NULL DEFAULT NULL,
  `macd` decimal(18, 2) NULL DEFAULT NULL,
  `dif` decimal(18, 2) NULL DEFAULT NULL,
  `dea` decimal(18, 2) NULL DEFAULT NULL,
  `cci` decimal(18, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `ts_code`(`ts_code`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `sshy`(`sshy`) USING BTREE,
  INDEX `market_capital`(`market_capital`) USING BTREE,
  INDEX `markettype`(`markettype`) USING BTREE,
  INDEX `type`(`type`) USING BTREE,
  INDEX `is_follow`(`is_follow`) USING BTREE,
  INDEX `symbol`(`symbol`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12602 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- View structure for v_a_common_stock_2018
-- ----------------------------
DROP VIEW IF EXISTS `v_a_common_stock_2018`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `v_a_common_stock_2018` AS select `b`.`REPORTDATE` AS `REPORTDATE`,`s`.`ts_code` AS `ts_code`,`s`.`pe_ttm` AS `pe_ttm`,`divide`(`s`.`market_capital`,`ic`.`KCFJCXSYJLR`) AS `kf_pe`,`convert_to_yiyuan`(((((((((((((ifnull(`b`.`MONETARYFUND`,0) + ifnull(`b`.`FVALUEFASSET`,0)) + ifnull(`b`.`DEFINEFVALUEFASSET`,0)) + ifnull(`b`.`INTERESTREC`,0)) + ifnull(`b`.`DIVIDENDREC`,0)) + ifnull(`b`.`BUYSELLBACKFASSET`,0)) + ifnull(`b`.`CLHELDSALEASS`,0)) + ifnull(`b`.`LOANADVANCES`,0)) + ifnull(`b`.`SALEABLEFASSET`,0)) + ifnull(`b`.`HELDMATURITYINV`,0)) + ifnull(`b`.`SELLBUYBACKFASSET`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then 0 else ifnull(`b`.`OTHERLASSET`,0) end)) + ifnull(`b`.`TRADEFASSET`,0))) AS `jrzc_noeastinv`,`s`.`pb` AS `pb`,`chg`.`chg` AS `chg`,`s`.`industry` AS `industry`,`s`.`name` AS `name`,timestampdiff(YEAR,`s`.`list_date`,curdate()) AS `list_date`,`convert_to_yiyuan`(`s`.`market_capital`) AS `market_capital`,cast((((`ic`.`OPERATEREVE` - `ic`.`OPERATEEXP`) / `ic`.`OPERATEREVE`) * 100) as decimal(18,2)) AS `gross_margin`,`convert_to_yiyuan`((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC_TOTAL`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end))) AS `yyzc`,`convert_to_yiyuan`(((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY_TOTAL`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0))) AS `yyfz`,`convert_to_yiyuan`(((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC_TOTAL`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end)) - ((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY_TOTAL`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0)))) AS `yyzbxq`,cast(((`ic`.`KCFJCXSYJLR` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `kf_roe`,cast(((`ic`.`PARENTNETPROFIT` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `roe`,`convert_to_yiyuan`(`b`.`SUMASSET`) AS `sumasset`,`calc_growth`(`b2`.`SUMASSET`,`b`.`SUMASSET`) AS `sumasset_yoy`,`convert_to_yiyuan`((((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0)) + ifnull(`cf`.`fixedassescraloss`,0))) AS `non_cash_cost`,`convert_to_yiyuan`(`cf`.`NETOPERATECASHFLOW`) AS `net_oper_cashflow`,`convert_to_yiyuan`(`ic`.`KCFJCXSYJLR`) AS `KCFJCXSYJLR`,`convert_to_yiyuan`(`hd`.`rdspendsum`) AS `rdspendsum`,`hd`.`rdspendsum_ratio` AS `rdspendsum_ratio`,`convert_to_yiyuan`(`hd`.`rdinvest`) AS `rdinvest`,`hd`.`rdinvest_ratio` AS `rdinvest_ratio`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `yysr`,`convert_to_yiyuan`(((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0))) AS `zbzc`,`convert_to_yiyuan`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0))) AS `oper_liab`,`calc_proportion`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0)),`b`.`SUMASSET`) AS `oper_liab_ratio`,`convert_to_yiyuan`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0))) AS `yxfz`,`calc_proportion`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0)),`b`.`SUMASSET`) AS `yxfz_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0))) AS `sh_funding`,`calc_proportion`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0)),`b`.`SUMASSET`) AS `sh_funding_ratio`,`convert_to_yiyuan`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0))) AS `profit_accu`,`calc_proportion`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0)),`b`.`SUMASSET`) AS `profit_accu_ratio`,`convert_to_yiyuan`(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit`,`calc_proportion`(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0)),`ic`.`OPERATEREVE`) AS `core_profit_ratio`,`divide`(`cf`.`NETOPERATECASHFLOW`,((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit_earned_rate`,`s`.`sum_dividends` AS `sum_dividends`,((`s`.`sum_ipo` + `s`.`sum_pp`) + `s`.`sum_ri`) AS `sum_fina`,`convert_to_yiyuan`(`b`.`ACCOUNTREC`) AS `accountrec`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `OPERATEREVE`,`convert_to_yiyuan`(`ic`.`SUMPROFIT`) AS `sumprofit`,`calc_proportion`(`ic`.`SUMPROFIT`,`ic`.`OPERATEREVE`) AS `sumprofit_ratio`,`calc_growth`(`ic2`.`OPERATEREVE`,`ic`.`OPERATEREVE`) AS `yszs`,`calc_growth`(`ic2`.`KCFJCXSYJLR`,`ic`.`KCFJCXSYJLR`) AS `kfjlr_zs`,`calc_growth`(`b2`.`INVENTORY`,`b`.`INVENTORY`) AS `chzs`,`convert_to_yiyuan`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`)) AS `fcf2018`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`ic`.`OPERATEREVE`) AS `fcf2018_ratio`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`s`.`market_capital`) AS `mc_fcf2018_ratio`,`convert_to_yiyuan`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0))) AS `jy_fcf2018`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`ic`.`OPERATEREVE`) AS `jy_fcf2018_ratio`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`s`.`market_capital`) AS `mc_jy_fcf2018_ratio`,`convert_to_yiyuan`(`cf`.`BUYFILASSETPAY`) AS `buyfilassetpay`,`convert_to_yiyuan`(`cf`.`NETINVCASHFLOW`) AS `netinvcashflow`,`convert_to_yiyuan`(`cf`.`NETFINACASHFLOW`) AS `netfinacashflow`,`convert_to_yiyuan`(`cf`.`ACCEPTINVREC`) AS `acceptinvrec`,`convert_to_yiyuan`(`cf`.`LOANREC`) AS `loanrec`,`convert_to_yiyuan`(`cf`.`ISSUEBONDREC`) AS `issuebondrec`,`convert_to_yiyuan`(`cf`.`REPAYDEBTPAY`) AS `repaydebtpay`,`convert_to_yiyuan`(`cf`.`DIVIPROFITORINTPAY`) AS `diviprofitorintpay`,`convert_to_yiyuan`(`cf`.`CASHEQUIENDING`) AS `cashequiending`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`OTHERLASSET`,0)) + ifnull(`b`.`MONETARYFUND`,0))) AS `xianjin`,`convert_to_yiyuan`(((ifnull(`ic`.`INVESTINCOME`,0) + ifnull(`ic`.`INTREVE`,0)) + ifnull(`s`.`interest_income`,0))) AS `investincome`,`divide`((ifnull((ifnull(`b`.`BILLREC`,0) + `b`.`OTHERLASSET`),0) + ifnull(`b`.`MONETARYFUND`,0)),((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0))) AS `dqczfx`,`convert_to_yiyuan`(`ic`.`NETPROFIT`) AS `netprofit`,`calc_proportion`(`ic`.`KCFJCXSYJLR`,`ic`.`OPERATEREVE`) AS `np_margin`,`convert_to_yiyuan`(`ic`.`SALEEXP`) AS `saleexp`,`convert_to_yiyuan`((ifnull(`ic`.`MANAGEEXP`,0) + ifnull(`ic`.`RDEXP`,0))) AS `manageexp`,`convert_to_yiyuan`(`ic`.`RDEXP`) AS `rdexp`,`convert_to_yiyuan`(`ic`.`FINANCEEXP`) AS `financeexp`,`convert_to_yiyuan`((((ifnull(`ic`.`SALEEXP`,0) + ifnull(`ic`.`MANAGEEXP`,0)) + ifnull(`ic`.`RDEXP`,0)) + ifnull(`ic`.`FINANCEEXP`,0))) AS `sum_exp`,`convert_to_yiyuan`(`b`.`SUMSHEQUITY`) AS `sumshequity`,`convert_to_yiyuan`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0))) AS `int_and_dev`,`calc_proportion`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0)),`b`.`SUMASSET`) AS `int_and_dev_ratio`,`convert_to_yiyuan`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0))) AS `fix`,`calc_proportion`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0)),`b`.`SUMASSET`) AS `fix_ratio`,`convert_to_yiyuan`(`b`.`MONETARYFUND`) AS `monetaryfund`,`calc_proportion`(`b`.`MONETARYFUND`,`b`.`SUMASSET`) AS `mf_ratio`,`convert_to_yiyuan`(`b`.`OTHERLASSET`) AS `otherlasset`,`calc_proportion`(`b`.`OTHERLASSET`,`b`.`SUMASSET`) AS `otherlasset_ratio`,`convert_to_yiyuan`(`b`.`GOODWILL`) AS `goodwill`,`convert_to_yiyuan`((ifnull(`b`.`GOODWILL`,0) - ifnull(`b2`.`GOODWILL`,0))) AS `goodwill_inc`,`calc_proportion`(`b`.`GOODWILL`,`b`.`SUMASSET`) AS `goodwill_ratio`,`convert_to_yiyuan`(`cf`.`DISPOSALINVREC`) AS `disposalinvrec`,`convert_to_yiyuan`(`cf`.`DISPFILASSETREC`) AS `dispfilassetrec`,`convert_to_yiyuan`(`cf`.`DISPSUBSIDIARYREC`) AS `dispsubsidiaryrec`,`convert_to_yiyuan`(`cf`.`INVPAY`) AS `invpay`,`convert_to_yiyuan`(`cf`.`GETSUBSIDIARYPAY`) AS `getsubsidiarypay`,`convert_to_yiyuan`(`cf`.`INVINCOMEREC`) AS `invincomerec`,`convert_to_yiyuan`((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0))) AS `bill_and_account_rec`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) - (ifnull(`b2`.`BILLREC`,0) + ifnull(`b2`.`ACCOUNTREC`,0)))) AS `bill_and_account_rec_inc`,`convert_to_yiyuan`((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0))) AS `bill_and_account_pay`,`convert_to_yiyuan`(((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) - (ifnull(`b2`.`BILLPAY`,0) + ifnull(`b2`.`ACCOUNTPAY`,0)))) AS `bill_and_account_pay_inc`,`convert_to_yiyuan`(`b`.`ADVANCEPAY`) AS `advancepay`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCEPAY`,0) - ifnull(`b2`.`ADVANCEPAY`,0))) AS `advancepay_inc`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0))) AS `adv`,`calc_proportion`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)),`b`.`SUMASSET`) AS `adv_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)) - (ifnull(`b2`.`ADVANCERECEIVE`,0) + ifnull(`b2`.`CONTRACTLIAB`,0)))) AS `adv_inc`,`convert_to_yiyuan`(`b`.`INVENTORY`) AS `inventory`,`calc_proportion`(`b`.`INVENTORY`,`b`.`SUMASSET`) AS `inv_ratio`,`convert_to_yiyuan`((ifnull(`b`.`INVENTORY`,0) - ifnull(`b2`.`INVENTORY`,0))) AS `inv_inc`,`convert_to_yiyuan`(`ic`.`NONOPERATEREVE`) AS `nonoperatereve` from (((((((`em_balancesheet_common` `b` left join `stock_basic` `s` on((`s`.`ts_code` = `b`.`SECURITYCODE`))) left join `static_chginyear` `chg` on((`chg`.`ts_code` = `s`.`ts_code`))) left join `ths_hd` `hd` on(((`hd`.`ts_code` = `s`.`ts_code`) and (`hd`.`reportdate` = '2018-12-31')))) left join `em_income_common` `ic` on(((`ic`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_cashflow_common` `cf` on(((`cf`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`cf`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_balancesheet_common` `b2` on(((`b2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`b2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) left join `em_income_common` `ic2` on(((`ic2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) where ((1 = 1) and (`b`.`REPORTDATE` = '2018-12-31')) order by `s`.`market_capital` desc;

-- ----------------------------
-- View structure for v_a_common_stock_2019
-- ----------------------------
DROP VIEW IF EXISTS `v_a_common_stock_2019`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `v_a_common_stock_2019` AS select `b`.`REPORTDATE` AS `REPORTDATE`,`s`.`ts_code` AS `ts_code`,`s`.`roe_gt15_count` AS `roe_gt15_count`,`s`.`pe_ttm` AS `pe_ttm`,`divide`(`s`.`market_capital`,`ic`.`KCFJCXSYJLR`) AS `kf_pe`,`s`.`pb` AS `pb`,`chg`.`chg` AS `chg`,`s`.`high52w` AS `high52w`,`s`.`industry` AS `industry`,`s`.`name` AS `name`,timestampdiff(YEAR,`s`.`list_date`,curdate()) AS `list_date`,`convert_to_yiyuan`(`s`.`market_capital`) AS `market_capital`,cast((((`ic`.`OPERATEREVE` - `ic`.`OPERATEEXP`) / `ic`.`OPERATEREVE`) * 100) as decimal(18,2)) AS `gross_margin`,`convert_to_yiyuan`((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end))) AS `yyzc`,`convert_to_yiyuan`(((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0))) AS `yyfz`,`convert_to_yiyuan`(((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end)) - ((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0)))) AS `yyzbxq`,`convert_to_yiyuan`(((((((((((((ifnull(`b`.`MONETARYFUND`,0) + ifnull(`b`.`FVALUEFASSET`,0)) + ifnull(`b`.`DEFINEFVALUEFASSET`,0)) + ifnull(`b`.`INTERESTREC`,0)) + ifnull(`b`.`DIVIDENDREC`,0)) + ifnull(`b`.`BUYSELLBACKFASSET`,0)) + ifnull(`b`.`CLHELDSALEASS`,0)) + ifnull(`b`.`LOANADVANCES`,0)) + ifnull(`b`.`SALEABLEFASSET`,0)) + ifnull(`b`.`HELDMATURITYINV`,0)) + ifnull(`b`.`SELLBUYBACKFASSET`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then 0 else ifnull(`b`.`OTHERLASSET`,0) end)) + ifnull(`b`.`TRADEFASSET`,0))) AS `jrzc_noeastinv`,cast(((`ic`.`KCFJCXSYJLR` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `kf_roe`,cast(((`ic`.`PARENTNETPROFIT` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `roe`,`convert_to_yiyuan`(`b`.`SUMASSET`) AS `sumasset`,`calc_growth`(`b2`.`SUMASSET`,`b`.`SUMASSET`) AS `sumasset_yoy`,`convert_to_yiyuan`((((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0)) + ifnull(`cf`.`fixedassescraloss`,0))) AS `non_cash_cost`,`convert_to_yiyuan`(`cf`.`NETOPERATECASHFLOW`) AS `net_oper_cashflow`,`convert_to_yiyuan`(`ic`.`KCFJCXSYJLR`) AS `KCFJCXSYJLR`,`convert_to_yiyuan`(`hd`.`rdspendsum`) AS `rdspendsum`,`hd`.`rdspendsum_ratio` AS `rdspendsum_ratio`,`convert_to_yiyuan`(`hd`.`rdinvest`) AS `rdinvest`,`hd`.`rdinvest_ratio` AS `rdinvest_ratio`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `yysr`,`convert_to_yiyuan`(((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0))) AS `zbzc`,`convert_to_yiyuan`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0))) AS `oper_liab`,`calc_proportion`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0)),`b`.`SUMASSET`) AS `oper_liab_ratio`,`convert_to_yiyuan`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0))) AS `yxfz`,`calc_proportion`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0)),`b`.`SUMASSET`) AS `yxfz_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0))) AS `sh_funding`,`calc_proportion`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0)),`b`.`SUMASSET`) AS `sh_funding_ratio`,`convert_to_yiyuan`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0))) AS `profit_accu`,`calc_proportion`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0)),`b`.`SUMASSET`) AS `profit_accu_ratio`,`convert_to_yiyuan`((((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`RDEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit`,`calc_proportion`((((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`RDEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0)),`ic`.`OPERATEREVE`) AS `core_profit_ratio`,`divide`(`cf`.`NETOPERATECASHFLOW`,(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`RDEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit_earned_rate`,`s`.`sum_dividends` AS `sum_dividends`,((`s`.`sum_ipo` + `s`.`sum_pp`) + `s`.`sum_ri`) AS `sum_fina`,`convert_to_yiyuan`(`b`.`ACCOUNTREC`) AS `accountrec`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `OPERATEREVE`,`convert_to_yiyuan`(`ic`.`SUMPROFIT`) AS `sumprofit`,`calc_proportion`(`ic`.`SUMPROFIT`,`ic`.`OPERATEREVE`) AS `sumprofit_ratio`,`calc_growth`(`ic2`.`OPERATEREVE`,`ic`.`OPERATEREVE`) AS `yszs`,`calc_growth`(`ic2`.`KCFJCXSYJLR`,`ic`.`KCFJCXSYJLR`) AS `kfjlr_zs`,`calc_growth`(`b2`.`INVENTORY`,`b`.`INVENTORY`) AS `chzs`,`convert_to_yiyuan`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`)) AS `fcf`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`ic`.`OPERATEREVE`) AS `fcf_ratio`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`s`.`market_capital`) AS `mc_fcf_ratio`,`convert_to_yiyuan`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0))) AS `jy_fcf`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`ic`.`OPERATEREVE`) AS `jy_fcf_ratio`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`s`.`market_capital`) AS `mc_jy_fcf_ratio`,`convert_to_yiyuan`(`cf`.`BUYFILASSETPAY`) AS `buyfilassetpay`,`convert_to_yiyuan`(`cf`.`NETINVCASHFLOW`) AS `netinvcashflow`,`convert_to_yiyuan`(`cf`.`NETFINACASHFLOW`) AS `netfinacashflow`,`convert_to_yiyuan`(`cf`.`ACCEPTINVREC`) AS `acceptinvrec`,`convert_to_yiyuan`(`cf`.`LOANREC`) AS `loanrec`,`convert_to_yiyuan`(`cf`.`ISSUEBONDREC`) AS `issuebondrec`,`convert_to_yiyuan`(`cf`.`REPAYDEBTPAY`) AS `repaydebtpay`,`convert_to_yiyuan`(`cf`.`DIVIPROFITORINTPAY`) AS `diviprofitorintpay`,`convert_to_yiyuan`(`cf`.`CASHEQUIENDING`) AS `cashequiending`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`OTHERLASSET`,0)) + ifnull(`b`.`MONETARYFUND`,0))) AS `xianjin`,`convert_to_yiyuan`(((ifnull(`ic`.`INVESTINCOME`,0) + ifnull(`ic`.`INTREVE`,0)) + ifnull(`s`.`interest_income`,0))) AS `investincome`,`divide`((ifnull((ifnull(`b`.`BILLREC`,0) + `b`.`OTHERLASSET`),0) + ifnull(`b`.`MONETARYFUND`,0)),((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0))) AS `dqczfx`,`convert_to_yiyuan`(`ic`.`NETPROFIT`) AS `netprofit`,`calc_proportion`(`ic`.`KCFJCXSYJLR`,`ic`.`OPERATEREVE`) AS `np_margin`,`convert_to_yiyuan`(`ic`.`SALEEXP`) AS `saleexp`,`convert_to_yiyuan`((ifnull(`ic`.`MANAGEEXP`,0) + ifnull(`ic`.`RDEXP`,0))) AS `manageexp`,`convert_to_yiyuan`(`ic`.`RDEXP`) AS `rdexp`,`convert_to_yiyuan`(`ic`.`FINANCEEXP`) AS `financeexp`,`convert_to_yiyuan`((((ifnull(`ic`.`SALEEXP`,0) + ifnull(`ic`.`MANAGEEXP`,0)) + ifnull(`ic`.`RDEXP`,0)) + ifnull(`ic`.`FINANCEEXP`,0))) AS `sum_exp`,`convert_to_yiyuan`(`b`.`SUMSHEQUITY`) AS `sumshequity`,`convert_to_yiyuan`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0))) AS `int_and_dev`,`calc_proportion`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0)),`b`.`SUMASSET`) AS `int_and_dev_ratio`,`convert_to_yiyuan`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0))) AS `fix`,`calc_proportion`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0)),`b`.`SUMASSET`) AS `fix_ratio`,`convert_to_yiyuan`(`b`.`MONETARYFUND`) AS `monetaryfund`,`calc_proportion`(`b`.`MONETARYFUND`,`b`.`SUMASSET`) AS `mf_ratio`,`convert_to_yiyuan`(`b`.`OTHERLASSET`) AS `otherlasset`,`calc_proportion`(`b`.`OTHERLASSET`,`b`.`SUMASSET`) AS `otherlasset_ratio`,`convert_to_yiyuan`(`b`.`GOODWILL`) AS `goodwill`,`convert_to_yiyuan`((ifnull(`b`.`GOODWILL`,0) - ifnull(`b2`.`GOODWILL`,0))) AS `goodwill_inc`,`calc_proportion`(`b`.`GOODWILL`,`b`.`SUMASSET`) AS `goodwill_ratio`,`convert_to_yiyuan`(`cf`.`DISPOSALINVREC`) AS `disposalinvrec`,`convert_to_yiyuan`(`cf`.`DISPFILASSETREC`) AS `dispfilassetrec`,`convert_to_yiyuan`(`cf`.`DISPSUBSIDIARYREC`) AS `dispsubsidiaryrec`,`convert_to_yiyuan`(`cf`.`INVPAY`) AS `invpay`,`convert_to_yiyuan`(`cf`.`GETSUBSIDIARYPAY`) AS `getsubsidiarypay`,`convert_to_yiyuan`(`cf`.`INVINCOMEREC`) AS `invincomerec`,`convert_to_yiyuan`((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0))) AS `bill_and_account_rec`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) - (ifnull(`b2`.`BILLREC`,0) + ifnull(`b2`.`ACCOUNTREC`,0)))) AS `bill_and_account_rec_inc`,`convert_to_yiyuan`((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0))) AS `bill_and_account_pay`,`convert_to_yiyuan`(((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) - (ifnull(`b2`.`BILLPAY`,0) + ifnull(`b2`.`ACCOUNTPAY`,0)))) AS `bill_and_account_pay_inc`,`convert_to_yiyuan`(`b`.`ADVANCEPAY`) AS `advancepay`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCEPAY`,0) - ifnull(`b2`.`ADVANCEPAY`,0))) AS `advancepay_inc`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0))) AS `adv`,`calc_proportion`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)),`b`.`SUMASSET`) AS `adv_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)) - (ifnull(`b2`.`ADVANCERECEIVE`,0) + ifnull(`b2`.`CONTRACTLIAB`,0)))) AS `adv_inc`,`convert_to_yiyuan`(`b`.`INVENTORY`) AS `inventory`,`calc_proportion`(`b`.`INVENTORY`,`b`.`SUMASSET`) AS `inv_ratio`,`convert_to_yiyuan`((ifnull(`b`.`INVENTORY`,0) - ifnull(`b2`.`INVENTORY`,0))) AS `inv_inc`,`convert_to_yiyuan`(`ic`.`NONOPERATEREVE`) AS `nonoperatereve` from (((((((`em_balancesheet_common` `b` left join `stock_basic` `s` on((`s`.`ts_code` = `b`.`SECURITYCODE`))) left join `static_chginyear` `chg` on((`chg`.`ts_code` = `s`.`ts_code`))) left join `ths_hd` `hd` on(((`hd`.`ts_code` = `s`.`ts_code`) and (`hd`.`reportdate` = '2018-12-31')))) left join `em_income_common` `ic` on(((`ic`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_cashflow_common` `cf` on(((`cf`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`cf`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_balancesheet_common` `b2` on(((`b2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`b2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) left join `em_income_common` `ic2` on(((`ic2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) where ((1 = 1) and (`b`.`REPORTDATE` = '2019-12-31')) order by `s`.`market_capital` desc;

-- ----------------------------
-- View structure for v_a_common_stock_2020
-- ----------------------------
DROP VIEW IF EXISTS `v_a_common_stock_2020`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `v_a_common_stock_2020` AS select `b`.`REPORTDATE` AS `REPORTDATE`,`s`.`ts_code` AS `ts_code`,`s`.`pe_ttm` AS `pe_ttm`,`divide`(`s`.`market_capital`,`ic`.`KCFJCXSYJLR`) AS `kf_pe`,`s`.`pb` AS `pb`,`chg`.`chg` AS `chg`,`s`.`industry` AS `industry`,`s`.`name` AS `name`,timestampdiff(YEAR,`s`.`list_date`,curdate()) AS `list_date`,`convert_to_yiyuan`(`s`.`market_capital`) AS `market_capital`,cast((((`ic`.`OPERATEREVE` - `ic`.`OPERATEEXP`) / `ic`.`OPERATEREVE`) * 100) as decimal(18,2)) AS `gross_margin`,`convert_to_yiyuan`(((((((((((((ifnull(`b`.`MONETARYFUND`,0) + ifnull(`b`.`FVALUEFASSET`,0)) + ifnull(`b`.`DEFINEFVALUEFASSET`,0)) + ifnull(`b`.`INTERESTREC`,0)) + ifnull(`b`.`DIVIDENDREC`,0)) + ifnull(`b`.`BUYSELLBACKFASSET`,0)) + ifnull(`b`.`CLHELDSALEASS`,0)) + ifnull(`b`.`LOANADVANCES`,0)) + ifnull(`b`.`SALEABLEFASSET`,0)) + ifnull(`b`.`HELDMATURITYINV`,0)) + ifnull(`b`.`SELLBUYBACKFASSET`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then 0 else ifnull(`b`.`OTHERLASSET`,0) end)) + ifnull(`b`.`TRADEFASSET`,0))) AS `jrzc_noeastinv`,`convert_to_yiyuan`((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC_TOTAL`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end))) AS `yyzc`,`convert_to_yiyuan`(((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY_TOTAL`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0))) AS `yyfz`,`convert_to_yiyuan`(((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC_TOTAL`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end)) - ((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY_TOTAL`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0)))) AS `yyzbxq`,cast(((`ic`.`KCFJCXSYJLR` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `kf_roe`,cast(((`ic`.`PARENTNETPROFIT` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `roe`,`convert_to_yiyuan`(`b`.`SUMASSET`) AS `sumasset`,`calc_growth`(`b2`.`SUMASSET`,`b`.`SUMASSET`) AS `sumasset_yoy`,`convert_to_yiyuan`((((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0)) + ifnull(`cf`.`fixedassescraloss`,0))) AS `non_cash_cost`,`convert_to_yiyuan`(`cf`.`NETOPERATECASHFLOW`) AS `net_oper_cashflow`,`convert_to_yiyuan`(`ic`.`KCFJCXSYJLR`) AS `KCFJCXSYJLR`,`convert_to_yiyuan`(`hd`.`rdspendsum`) AS `rdspendsum`,`hd`.`rdspendsum_ratio` AS `rdspendsum_ratio`,`convert_to_yiyuan`(`hd`.`rdinvest`) AS `rdinvest`,`hd`.`rdinvest_ratio` AS `rdinvest_ratio`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `yysr`,`convert_to_yiyuan`(((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0))) AS `zbzc`,`convert_to_yiyuan`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0))) AS `oper_liab`,`calc_proportion`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0)),`b`.`SUMASSET`) AS `oper_liab_ratio`,`convert_to_yiyuan`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0))) AS `yxfz`,`calc_proportion`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0)),`b`.`SUMASSET`) AS `yxfz_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0))) AS `sh_funding`,`calc_proportion`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0)),`b`.`SUMASSET`) AS `sh_funding_ratio`,`convert_to_yiyuan`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0))) AS `profit_accu`,`calc_proportion`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0)),`b`.`SUMASSET`) AS `profit_accu_ratio`,`convert_to_yiyuan`(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit`,`calc_proportion`(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0)),`ic`.`OPERATEREVE`) AS `core_profit_ratio`,`divide`(`cf`.`NETOPERATECASHFLOW`,((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit_earned_rate`,`s`.`sum_dividends` AS `sum_dividends`,((`s`.`sum_ipo` + `s`.`sum_pp`) + `s`.`sum_ri`) AS `sum_fina`,`convert_to_yiyuan`(`b`.`ACCOUNTREC`) AS `accountrec`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `OPERATEREVE`,`convert_to_yiyuan`(`ic`.`SUMPROFIT`) AS `sumprofit`,`calc_proportion`(`ic`.`SUMPROFIT`,`ic`.`OPERATEREVE`) AS `sumprofit_ratio`,`calc_growth`(`ic2`.`OPERATEREVE`,`ic`.`OPERATEREVE`) AS `yszs`,`calc_growth`(`ic2`.`KCFJCXSYJLR`,`ic`.`KCFJCXSYJLR`) AS `kfjlr_zs`,`calc_growth`(`b2`.`INVENTORY`,`b`.`INVENTORY`) AS `chzs`,`convert_to_yiyuan`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`)) AS `fcf2018`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`ic`.`OPERATEREVE`) AS `fcf2018_ratio`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`s`.`market_capital`) AS `mc_fcf2018_ratio`,`convert_to_yiyuan`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0))) AS `jy_fcf2018`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`ic`.`OPERATEREVE`) AS `jy_fcf2018_ratio`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`s`.`market_capital`) AS `mc_jy_fcf2018_ratio`,`convert_to_yiyuan`(`cf`.`BUYFILASSETPAY`) AS `buyfilassetpay`,`convert_to_yiyuan`(`cf`.`NETINVCASHFLOW`) AS `netinvcashflow`,`convert_to_yiyuan`(`cf`.`NETFINACASHFLOW`) AS `netfinacashflow`,`convert_to_yiyuan`(`cf`.`ACCEPTINVREC`) AS `acceptinvrec`,`convert_to_yiyuan`(`cf`.`LOANREC`) AS `loanrec`,`convert_to_yiyuan`(`cf`.`ISSUEBONDREC`) AS `issuebondrec`,`convert_to_yiyuan`(`cf`.`REPAYDEBTPAY`) AS `repaydebtpay`,`convert_to_yiyuan`(`cf`.`DIVIPROFITORINTPAY`) AS `diviprofitorintpay`,`convert_to_yiyuan`(`cf`.`CASHEQUIENDING`) AS `cashequiending`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`OTHERLASSET`,0)) + ifnull(`b`.`MONETARYFUND`,0))) AS `xianjin`,`convert_to_yiyuan`(((ifnull(`ic`.`INVESTINCOME`,0) + ifnull(`ic`.`INTREVE`,0)) + ifnull(`s`.`interest_income`,0))) AS `investincome`,`divide`((ifnull((ifnull(`b`.`BILLREC`,0) + `b`.`OTHERLASSET`),0) + ifnull(`b`.`MONETARYFUND`,0)),((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0))) AS `dqczfx`,`convert_to_yiyuan`(`ic`.`NETPROFIT`) AS `netprofit`,`calc_proportion`(`ic`.`KCFJCXSYJLR`,`ic`.`OPERATEREVE`) AS `np_margin`,`convert_to_yiyuan`(`ic`.`SALEEXP`) AS `saleexp`,`convert_to_yiyuan`((ifnull(`ic`.`MANAGEEXP`,0) + ifnull(`ic`.`RDEXP`,0))) AS `manageexp`,`convert_to_yiyuan`(`ic`.`RDEXP`) AS `rdexp`,`convert_to_yiyuan`(`ic`.`FINANCEEXP`) AS `financeexp`,`convert_to_yiyuan`((((ifnull(`ic`.`SALEEXP`,0) + ifnull(`ic`.`MANAGEEXP`,0)) + ifnull(`ic`.`RDEXP`,0)) + ifnull(`ic`.`FINANCEEXP`,0))) AS `sum_exp`,`convert_to_yiyuan`(`b`.`SUMSHEQUITY`) AS `sumshequity`,`convert_to_yiyuan`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0))) AS `int_and_dev`,`calc_proportion`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0)),`b`.`SUMASSET`) AS `int_and_dev_ratio`,`convert_to_yiyuan`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0))) AS `fix`,`calc_proportion`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0)),`b`.`SUMASSET`) AS `fix_ratio`,`convert_to_yiyuan`(`b`.`MONETARYFUND`) AS `monetaryfund`,`calc_proportion`(`b`.`MONETARYFUND`,`b`.`SUMASSET`) AS `mf_ratio`,`convert_to_yiyuan`(`b`.`OTHERLASSET`) AS `otherlasset`,`calc_proportion`(`b`.`OTHERLASSET`,`b`.`SUMASSET`) AS `otherlasset_ratio`,`convert_to_yiyuan`(`b`.`GOODWILL`) AS `goodwill`,`convert_to_yiyuan`((ifnull(`b`.`GOODWILL`,0) - ifnull(`b2`.`GOODWILL`,0))) AS `goodwill_inc`,`calc_proportion`(`b`.`GOODWILL`,`b`.`SUMASSET`) AS `goodwill_ratio`,`convert_to_yiyuan`(`cf`.`DISPOSALINVREC`) AS `disposalinvrec`,`convert_to_yiyuan`(`cf`.`DISPFILASSETREC`) AS `dispfilassetrec`,`convert_to_yiyuan`(`cf`.`DISPSUBSIDIARYREC`) AS `dispsubsidiaryrec`,`convert_to_yiyuan`(`cf`.`INVPAY`) AS `invpay`,`convert_to_yiyuan`(`cf`.`GETSUBSIDIARYPAY`) AS `getsubsidiarypay`,`convert_to_yiyuan`(`cf`.`INVINCOMEREC`) AS `invincomerec`,`convert_to_yiyuan`((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0))) AS `bill_and_account_rec`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) - (ifnull(`b2`.`BILLREC`,0) + ifnull(`b2`.`ACCOUNTREC`,0)))) AS `bill_and_account_rec_inc`,`convert_to_yiyuan`((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0))) AS `bill_and_account_pay`,`convert_to_yiyuan`(((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) - (ifnull(`b2`.`BILLPAY`,0) + ifnull(`b2`.`ACCOUNTPAY`,0)))) AS `bill_and_account_pay_inc`,`convert_to_yiyuan`(`b`.`ADVANCEPAY`) AS `advancepay`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCEPAY`,0) - ifnull(`b2`.`ADVANCEPAY`,0))) AS `advancepay_inc`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0))) AS `adv`,`calc_proportion`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)),`b`.`SUMASSET`) AS `adv_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)) - (ifnull(`b2`.`ADVANCERECEIVE`,0) + ifnull(`b2`.`CONTRACTLIAB`,0)))) AS `adv_inc`,`convert_to_yiyuan`(`b`.`INVENTORY`) AS `inventory`,`calc_proportion`(`b`.`INVENTORY`,`b`.`SUMASSET`) AS `inv_ratio`,`convert_to_yiyuan`((ifnull(`b`.`INVENTORY`,0) - ifnull(`b2`.`INVENTORY`,0))) AS `inv_inc`,`convert_to_yiyuan`(`ic`.`NONOPERATEREVE`) AS `nonoperatereve` from (((((((`em_balancesheet_common` `b` left join `stock_basic` `s` on((`s`.`ts_code` = `b`.`SECURITYCODE`))) left join `static_chginyear` `chg` on((`chg`.`ts_code` = `s`.`ts_code`))) left join `ths_hd` `hd` on(((`hd`.`ts_code` = `s`.`ts_code`) and (`hd`.`reportdate` = '2018-12-31')))) left join `em_income_common` `ic` on(((`ic`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_cashflow_common` `cf` on(((`cf`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`cf`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_balancesheet_common` `b2` on(((`b2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`b2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) left join `em_income_common` `ic2` on(((`ic2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) where ((1 = 1) and (`b`.`REPORTDATE` = '2020-03-31')) order by `s`.`market_capital` desc;

-- ----------------------------
-- View structure for v_a_common_stock_all
-- ----------------------------
DROP VIEW IF EXISTS `v_a_common_stock_all`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `v_a_common_stock_all` AS select `b`.`REPORTDATE` AS `REPORTDATE`,`s`.`ts_code` AS `ts_code`,`s`.`pe_ttm` AS `pe_ttm`,`divide`(`s`.`market_capital`,`ic`.`KCFJCXSYJLR`) AS `kf_pe`,`s`.`pb` AS `pb`,`chg`.`chg` AS `chg`,`s`.`industry` AS `industry`,`s`.`name` AS `name`,timestampdiff(YEAR,`s`.`list_date`,curdate()) AS `list_date`,`convert_to_yiyuan`(`s`.`market_capital`) AS `market_capital`,cast((((`ic`.`OPERATEREVE` - `ic`.`OPERATEEXP`) / `ic`.`OPERATEREVE`) * 100) as decimal(18,2)) AS `gross_margin`,`convert_to_yiyuan`(((((((((((((ifnull(`b`.`MONETARYFUND`,0) + ifnull(`b`.`FVALUEFASSET`,0)) + ifnull(`b`.`DEFINEFVALUEFASSET`,0)) + ifnull(`b`.`INTERESTREC`,0)) + ifnull(`b`.`DIVIDENDREC`,0)) + ifnull(`b`.`BUYSELLBACKFASSET`,0)) + ifnull(`b`.`CLHELDSALEASS`,0)) + ifnull(`b`.`LOANADVANCES`,0)) + ifnull(`b`.`SALEABLEFASSET`,0)) + ifnull(`b`.`HELDMATURITYINV`,0)) + ifnull(`b`.`SELLBUYBACKFASSET`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then 0 else ifnull(`b`.`OTHERLASSET`,0) end)) + ifnull(`b`.`TRADEFASSET`,0))) AS `jrzc_noeastinv`,`convert_to_yiyuan`((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC_TOTAL`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end))) AS `yyzc`,`convert_to_yiyuan`(((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY_TOTAL`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0))) AS `yyfz`,`convert_to_yiyuan`(((((((((((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) + ifnull(`b`.`ADVANCEPAY`,0)) + ifnull(`b`.`INVENTORY`,0)) + ifnull(`b`.`LTREC`,0)) + ifnull(`b`.`DEFERINCOMETAXASSET`,0)) + ifnull(`b`.`NONLASSETONEYEAR`,0)) + ifnull(`b`.`OTHERREC_TOTAL`,0)) + (case when (`s`.`industry` in ('全国地产','区域地产','建筑工程')) then ifnull(`b`.`OTHERLASSET`,0) else 0 end)) + (case when (`s`.`industry` = '建筑工程') then ifnull(`b`.`OTHERNONLASSET`,0) else 0 end)) - ((((((((((((((ifnull(`b`.`DEFINEFVALUEFLIAB`,0) + ifnull(`b`.`BILLPAY`,0)) + if((isnull(`b`.`BILLPAY`) and isnull(`b`.`ACCOUNTPAY`)),`b`.`BILLPAY_ACCOUNTPAY`,`b`.`ACCOUNTPAY`)) + if(((`b`.`ADVANCERECEIVE` <> NULL) or (`b`.`ADVANCERECEIVE` <> 0)),`b`.`ADVANCERECEIVE`,0)) + if(((`b`.`CONTRACTLIAB` <> NULL) or (`b`.`CONTRACTLIAB` <> 0)),`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`DEFERINCOME`,0)) + ifnull(`b`.`DEFERINCOMETAXLIAB`,0)) + ifnull(`b`.`LTSALARYPAY`,0)) + ifnull(`b`.`OTHERPAY_TOTAL`,0)) + ifnull(`b`.`DEPOSIT`,0)) + ifnull(`b`.`ANTICIPATELIAB`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`SPECIALPAY`,0)))) AS `yyzbxq`,(case when ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) <> 0) then cast(((`ic`.`KCFJCXSYJLR` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) else NULL end) AS `kf_roe`,cast(((`ic`.`PARENTNETPROFIT` / ((`b`.`SUMPARENTEQUITY` + `b2`.`SUMPARENTEQUITY`) / 2)) * 100) as decimal(18,2)) AS `roe`,`convert_to_yiyuan`(`b`.`SUMASSET`) AS `sumasset`,`calc_growth`(`b2`.`SUMASSET`,`b`.`SUMASSET`) AS `sumasset_yoy`,`convert_to_yiyuan`((((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0)) + ifnull(`cf`.`fixedassescraloss`,0))) AS `non_cash_cost`,`convert_to_yiyuan`(`cf`.`NETOPERATECASHFLOW`) AS `net_oper_cashflow`,`convert_to_yiyuan`(`ic`.`KCFJCXSYJLR`) AS `KCFJCXSYJLR`,`convert_to_yiyuan`(`hd`.`rdspendsum`) AS `rdspendsum`,`hd`.`rdspendsum_ratio` AS `rdspendsum_ratio`,`convert_to_yiyuan`(`hd`.`rdinvest`) AS `rdinvest`,`hd`.`rdinvest_ratio` AS `rdinvest_ratio`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `yysr`,`convert_to_yiyuan`(((((ifnull(`cf`.`asseimpa`,0) + ifnull(`cf`.`assedepr`,0)) + ifnull(`cf`.`intaasseamor`,0)) + ifnull(`cf`.`longdefeexpenamor`,0)) + ifnull(`cf`.`dispfixedassetloss`,0))) AS `zbzc`,`convert_to_yiyuan`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0))) AS `oper_liab`,`calc_proportion`((((((((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) + ifnull(`b`.`ADVANCERECEIVE`,0)) + ifnull(`b`.`CONTRACTLIAB`,0)) + ifnull(`b`.`SALARYPAY`,0)) + ifnull(`b`.`TAXPAY`,0)) + ifnull(`b`.`OTHERLLIAB`,0)) + ifnull(`b`.`OTHERPAY`,0)),`b`.`SUMASSET`) AS `oper_liab_ratio`,`convert_to_yiyuan`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0))) AS `yxfz`,`calc_proportion`(((((((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0)) + ifnull(`b`.`LTBORROW`,0)) + ifnull(`b`.`BONDPAY`,0)) + ifnull(`b`.`LTACCOUNTPAY`,0)) + ifnull(`b`.`SUSTAINABLEDEBT`,0)),`b`.`SUMASSET`) AS `yxfz_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0))) AS `sh_funding`,`calc_proportion`(((ifnull(`b`.`SHARECAPITAL`,0) + ifnull(`b`.`CAPITALRESERVE`,0)) + ifnull(`b`.`MINORITYEQUITY`,0)),`b`.`SUMASSET`) AS `sh_funding_ratio`,`convert_to_yiyuan`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0))) AS `profit_accu`,`calc_proportion`((ifnull(`b`.`SURPLUSRESERVE`,0) + ifnull(`b`.`RETAINEDEARNING`,0)),`b`.`SUMASSET`) AS `profit_accu_ratio`,`convert_to_yiyuan`(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit`,`calc_proportion`(((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0)),`ic`.`OPERATEREVE`) AS `core_profit_ratio`,`divide`(`cf`.`NETOPERATECASHFLOW`,((((((ifnull(`ic`.`OPERATEREVE`,0) - ifnull(`ic`.`OPERATEEXP`,0)) - ifnull(`ic`.`OPERATETAX`,0)) - ifnull(`ic`.`SALEEXP`,0)) - ifnull(`ic`.`MANAGEEXP`,0)) - ifnull(`ic`.`FINANCEEXP`,0)) - ifnull(`ic`.`ASSETDEVALUELOSS`,0))) AS `core_profit_earned_rate`,`s`.`sum_dividends` AS `sum_dividends`,((`s`.`sum_ipo` + `s`.`sum_pp`) + `s`.`sum_ri`) AS `sum_fina`,`convert_to_yiyuan`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0))) AS `jy_fcf`,`convert_to_yiyuan`(`b`.`ACCOUNTREC`) AS `accountrec`,`convert_to_yiyuan`(`ic`.`OPERATEREVE`) AS `OPERATEREVE`,`convert_to_yiyuan`(`ic`.`SUMPROFIT`) AS `sumprofit`,`calc_proportion`(`ic`.`SUMPROFIT`,`ic`.`OPERATEREVE`) AS `sumprofit_ratio`,`calc_growth`(`ic2`.`OPERATEREVE`,`ic`.`OPERATEREVE`) AS `yszs`,`calc_growth`(`ic2`.`KCFJCXSYJLR`,`ic`.`KCFJCXSYJLR`) AS `kfjlr_zs`,`calc_growth`(`b2`.`INVENTORY`,`b`.`INVENTORY`) AS `chzs`,`convert_to_yiyuan`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`)) AS `fcf2018`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`ic`.`OPERATEREVE`) AS `fcf2018_ratio`,`calc_proportion`((`cf`.`NETOPERATECASHFLOW` - `cf`.`BUYFILASSETPAY`),`s`.`market_capital`) AS `mc_fcf2018_ratio`,`convert_to_yiyuan`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0))) AS `jy_fcf2018`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`ic`.`OPERATEREVE`) AS `jy_fcf2018_ratio`,`calc_proportion`((((((ifnull(`cf`.`NETOPERATECASHFLOW`,0) - ifnull(`cf`.`asseimpa`,0)) - ifnull(`cf`.`assedepr`,0)) - ifnull(`cf`.`intaasseamor`,0)) - ifnull(`cf`.`longdefeexpenamor`,0)) - ifnull(`cf`.`dispfixedassetloss`,0)),`s`.`market_capital`) AS `mc_jy_fcf2018_ratio`,`convert_to_yiyuan`(`cf`.`BUYFILASSETPAY`) AS `buyfilassetpay`,`convert_to_yiyuan`(`cf`.`NETINVCASHFLOW`) AS `netinvcashflow`,`convert_to_yiyuan`(`cf`.`NETFINACASHFLOW`) AS `netfinacashflow`,`convert_to_yiyuan`(`cf`.`ACCEPTINVREC`) AS `acceptinvrec`,`convert_to_yiyuan`(`cf`.`LOANREC`) AS `loanrec`,`convert_to_yiyuan`(`cf`.`ISSUEBONDREC`) AS `issuebondrec`,`convert_to_yiyuan`(`cf`.`REPAYDEBTPAY`) AS `repaydebtpay`,`convert_to_yiyuan`(`cf`.`DIVIPROFITORINTPAY`) AS `diviprofitorintpay`,`convert_to_yiyuan`(`cf`.`CASHEQUIENDING`) AS `cashequiending`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`OTHERLASSET`,0)) + ifnull(`b`.`MONETARYFUND`,0))) AS `xianjin`,`convert_to_yiyuan`(((ifnull(`ic`.`INVESTINCOME`,0) + ifnull(`ic`.`INTREVE`,0)) + ifnull(`s`.`interest_income`,0))) AS `investincome`,`divide`((ifnull((ifnull(`b`.`BILLREC`,0) + `b`.`OTHERLASSET`),0) + ifnull(`b`.`MONETARYFUND`,0)),((ifnull(`b`.`STBORROW`,0) + ifnull(`b`.`NONLLIABONEYEAR`,0)) + ifnull(`b`.`TRADEFLIAB`,0))) AS `dqczfx`,`convert_to_yiyuan`(`ic`.`NETPROFIT`) AS `netprofit`,`calc_proportion`(`ic`.`KCFJCXSYJLR`,`ic`.`OPERATEREVE`) AS `np_margin`,`convert_to_yiyuan`(`ic`.`SALEEXP`) AS `saleexp`,`convert_to_yiyuan`((ifnull(`ic`.`MANAGEEXP`,0) + ifnull(`ic`.`RDEXP`,0))) AS `manageexp`,`convert_to_yiyuan`(`ic`.`RDEXP`) AS `rdexp`,`convert_to_yiyuan`(`ic`.`FINANCEEXP`) AS `financeexp`,`convert_to_yiyuan`((((ifnull(`ic`.`SALEEXP`,0) + ifnull(`ic`.`MANAGEEXP`,0)) + ifnull(`ic`.`RDEXP`,0)) + ifnull(`ic`.`FINANCEEXP`,0))) AS `sum_exp`,`convert_to_yiyuan`(`b`.`SUMSHEQUITY`) AS `sumshequity`,`convert_to_yiyuan`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0))) AS `int_and_dev`,`calc_proportion`((ifnull(`b`.`INTANGIBLEASSET`,0) + ifnull(`b`.`DEVELOPEXP`,0)),`b`.`SUMASSET`) AS `int_and_dev_ratio`,`convert_to_yiyuan`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0))) AS `fix`,`calc_proportion`((ifnull(`b`.`FIXEDASSET`,0) + ifnull(`b`.`CONSTRUCTIONPROGRESS`,0)),`b`.`SUMASSET`) AS `fix_ratio`,`convert_to_yiyuan`(`b`.`MONETARYFUND`) AS `monetaryfund`,`calc_proportion`(`b`.`MONETARYFUND`,`b`.`SUMASSET`) AS `mf_ratio`,`convert_to_yiyuan`(`b`.`OTHERLASSET`) AS `otherlasset`,`calc_proportion`(`b`.`OTHERLASSET`,`b`.`SUMASSET`) AS `otherlasset_ratio`,`convert_to_yiyuan`(`b`.`GOODWILL`) AS `goodwill`,`convert_to_yiyuan`((ifnull(`b`.`GOODWILL`,0) - ifnull(`b2`.`GOODWILL`,0))) AS `goodwill_inc`,`calc_proportion`(`b`.`GOODWILL`,`b`.`SUMASSET`) AS `goodwill_ratio`,`convert_to_yiyuan`(`cf`.`DISPOSALINVREC`) AS `disposalinvrec`,`convert_to_yiyuan`(`cf`.`DISPFILASSETREC`) AS `dispfilassetrec`,`convert_to_yiyuan`(`cf`.`DISPSUBSIDIARYREC`) AS `dispsubsidiaryrec`,`convert_to_yiyuan`(`cf`.`INVPAY`) AS `invpay`,`convert_to_yiyuan`(`cf`.`GETSUBSIDIARYPAY`) AS `getsubsidiarypay`,`convert_to_yiyuan`(`cf`.`INVINCOMEREC`) AS `invincomerec`,`convert_to_yiyuan`((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0))) AS `bill_and_account_rec`,`convert_to_yiyuan`(((ifnull(`b`.`BILLREC`,0) + ifnull(`b`.`ACCOUNTREC`,0)) - (ifnull(`b2`.`BILLREC`,0) + ifnull(`b2`.`ACCOUNTREC`,0)))) AS `bill_and_account_rec_inc`,`convert_to_yiyuan`((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0))) AS `bill_and_account_pay`,`convert_to_yiyuan`(((ifnull(`b`.`BILLPAY`,0) + ifnull(`b`.`ACCOUNTPAY`,0)) - (ifnull(`b2`.`BILLPAY`,0) + ifnull(`b2`.`ACCOUNTPAY`,0)))) AS `bill_and_account_pay_inc`,`convert_to_yiyuan`(`b`.`ADVANCEPAY`) AS `advancepay`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCEPAY`,0) - ifnull(`b2`.`ADVANCEPAY`,0))) AS `advancepay_inc`,`convert_to_yiyuan`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0))) AS `adv`,`calc_proportion`((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)),`b`.`SUMASSET`) AS `adv_ratio`,`convert_to_yiyuan`(((ifnull(`b`.`ADVANCERECEIVE`,0) + ifnull(`b`.`CONTRACTLIAB`,0)) - (ifnull(`b2`.`ADVANCERECEIVE`,0) + ifnull(`b2`.`CONTRACTLIAB`,0)))) AS `adv_inc`,`convert_to_yiyuan`(`b`.`INVENTORY`) AS `inventory`,`calc_proportion`(`b`.`INVENTORY`,`b`.`SUMASSET`) AS `inv_ratio`,`convert_to_yiyuan`((ifnull(`b`.`INVENTORY`,0) - ifnull(`b2`.`INVENTORY`,0))) AS `inv_inc`,`convert_to_yiyuan`(`ic`.`NONOPERATEREVE`) AS `nonoperatereve` from (((((((`em_balancesheet_common` `b` left join `stock_basic` `s` on((`s`.`ts_code` = `b`.`SECURITYCODE`))) left join `static_chginyear` `chg` on((`chg`.`ts_code` = `s`.`ts_code`))) left join `ths_hd` `hd` on(((`hd`.`ts_code` = `s`.`ts_code`) and (`hd`.`reportdate` = '2018-12-31')))) left join `em_income_common` `ic` on(((`ic`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_cashflow_common` `cf` on(((`cf`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`cf`.`REPORTDATE` = `b`.`REPORTDATE`)))) left join `em_balancesheet_common` `b2` on(((`b2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`b2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) left join `em_income_common` `ic2` on(((`ic2`.`SECURITYCODE` = `b`.`SECURITYCODE`) and (`ic2`.`REPORTDATE` = (`b`.`REPORTDATE` + interval '-1' year))))) order by `s`.`market_capital` desc;

-- ----------------------------
-- View structure for v_quant_stock
-- ----------------------------
DROP VIEW IF EXISTS `v_quant_stock`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `v_quant_stock` AS select `s2`.`secucode` AS `secucode`,`s2`.`sum_netcash_operate` AS `sum_netcash_operate`,`s2`.`avg_netcash_operate` AS `avg_netcash_operate`,`s2`.`sum_gsjlr` AS `sum_gsjlr`,`s2`.`avg_gsjlr` AS `avg_gsjlr`,`s2`.`sum_lxzc` AS `sum_lxzc`,`s2`.`sum_bonus` AS `sum_bonus`,`s`.`sshy` AS `sshy`,`s`.`name` AS `name`,`convert_to_yiyuan`(`s`.`market_capital`) AS `mc`,`m2`.`jqjzcsyl` AS `jqjzcsyl2020`,`m2`.`gsjlr` AS `gsjlr2020`,`m2`.`gsjlrtbzz` AS `gsjlrtbzz2020`,`m3`.`gsjlr` AS `gsjlr2021h1`,`m3`.`gsjlrtbzz` AS `gsjlrtbzz2021h1`,`m11`.`roe_gt15_count` AS `roe_gt15_count`,`calc_cagr`(`m31`.`gsjlr`,`m2`.`gsjlr`,4) AS `cagr_gsjlr_4years`,`calc_cagr`(`cf31`.`netcash_operate`,`cf2`.`netcash_operate`,4) AS `cagr_netcash_operate_4years` from (((((((((select `b`.`secucode` AS `secucode`,sum(`cf`.`netcash_operate`) AS `sum_netcash_operate`,avg(`cf`.`netcash_operate`) AS `avg_netcash_operate`,sum(`m`.`gsjlr`) AS `sum_gsjlr`,avg(`m`.`gsjlr`) AS `avg_gsjlr`,sum((`cf`.`assign_dividend_porfit` - `m`.`bonus`)) AS `sum_lxzc`,sum(`m`.`bonus`) AS `sum_bonus` from ((`niceshotdb`.`em_bal_common_v1` `b` left join `niceshotdb`.`em_a_mainindex` `m` on(((`m`.`ts_code` = `b`.`secucode`) and (`m`.`date` = `b`.`report_date`)))) left join `niceshotdb`.`em_cf_common_v1` `cf` on(((`cf`.`secucode` = `b`.`secucode`) and (`cf`.`report_date` = `b`.`report_date`)))) where ((1 = 1) and (month(`b`.`report_date`) = 12) and (`b`.`report_date` >= '2016-12-31')) group by `b`.`secucode`)) `s2` left join `niceshotdb`.`xq_stock` `s` on((`s`.`ts_code` = `s2`.`secucode`))) left join `niceshotdb`.`em_a_mainindex` `m2` on(((`s2`.`secucode` = `m2`.`ts_code`) and (`m2`.`date` = '2020-12-31')))) left join `niceshotdb`.`em_a_mainindex` `m3` on(((`s2`.`secucode` = `m3`.`ts_code`) and (`m3`.`date` = '2021-6-30')))) left join (select `m4`.`ts_code` AS `ts_code`,count(`m4`.`jqjzcsyl`) AS `roe_gt15_count` from `niceshotdb`.`em_a_mainindex` `m4` where ((month(`m4`.`date`) = 12) and (`m4`.`jqjzcsyl` >= 15) and (`m4`.`date` >= '2016-12-31')) group by `m4`.`ts_code`) `m11` on((`s2`.`secucode` = `m11`.`ts_code`))) left join `niceshotdb`.`em_a_mainindex` `m31` on(((`s2`.`secucode` = `m31`.`ts_code`) and (`m31`.`date` = '2016-12-31')))) left join `niceshotdb`.`em_cf_common_v1` `cf2` on(((`cf2`.`secucode` = `s2`.`secucode`) and (`cf2`.`report_date` = '2020-12-31')))) left join `niceshotdb`.`em_cf_common_v1` `cf31` on(((`cf31`.`secucode` = `s2`.`secucode`) and (`cf31`.`report_date` = '2016-12-31'))));

-- ----------------------------
-- Function structure for calc_cagr
-- ----------------------------
DROP FUNCTION IF EXISTS `calc_cagr`;
delimiter ;;
CREATE FUNCTION `calc_cagr`(val1 decimal(18,2),val2 decimal(18,2),years int)
 RETURNS decimal(18,2)
BEGIN
	return if((val2<=0 or val2 is null or val1<=0 or val1 is null),null,convert((power(val2/val1,1/years)-1)*100,decimal(18,2)));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for calc_growth
-- ----------------------------
DROP FUNCTION IF EXISTS `calc_growth`;
delimiter ;;
CREATE FUNCTION `calc_growth`(val1 decimal(18,2),val2 decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return if((val1=0 or val1 is null),null,convert((val2-val1)/abs(val1)*100,decimal(18,2)));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for calc_percent
-- ----------------------------
DROP FUNCTION IF EXISTS `calc_percent`;
delimiter ;;
CREATE FUNCTION `calc_percent`(val1 decimal(18,2),val2 decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return if((val2=0 or val2 is null),null,convert((val1-val2)/val2*100,decimal(18,2)));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for calc_proportion
-- ----------------------------
DROP FUNCTION IF EXISTS `calc_proportion`;
delimiter ;;
CREATE FUNCTION `calc_proportion`(val1 decimal(18,2),val2 decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return if((val2=0 or val2 is null or (val1 >0 and val2<0) or (val1<0 and val2>0) ),null,convert(val1/val2*100,decimal(18,2)));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for calc_yoy
-- ----------------------------
DROP FUNCTION IF EXISTS `calc_yoy`;
delimiter ;;
CREATE FUNCTION `calc_yoy`(val1 decimal(18,2),val2 decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return if((val2=0 or val2 is null),null,convert((val1-val2)/val2*100,decimal(18,2)));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for convert_to_decimal
-- ----------------------------
DROP FUNCTION IF EXISTS `convert_to_decimal`;
delimiter ;;
CREATE FUNCTION `convert_to_decimal`(val decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return convert(val,decimal(18,2));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for convert_to_wanyuan_ld
-- ----------------------------
DROP FUNCTION IF EXISTS `convert_to_wanyuan_ld`;
delimiter ;;
CREATE FUNCTION `convert_to_wanyuan_ld`(val decimal(18,2))
 RETURNS decimal(18,6)
BEGIN
	return convert(val/10000,decimal(18,6));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for convert_to_yiyuan
-- ----------------------------
DROP FUNCTION IF EXISTS `convert_to_yiyuan`;
delimiter ;;
CREATE FUNCTION `convert_to_yiyuan`(val decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return convert(val/100000000,decimal(18,2));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for convert_to_yiyuan_ld
-- ----------------------------
DROP FUNCTION IF EXISTS `convert_to_yiyuan_ld`;
delimiter ;;
CREATE FUNCTION `convert_to_yiyuan_ld`(val decimal(18,2))
 RETURNS decimal(18,10)
BEGIN
	return convert(val/100000000,decimal(18,10));
END
;;
delimiter ;

-- ----------------------------
-- Function structure for divide
-- ----------------------------
DROP FUNCTION IF EXISTS `divide`;
delimiter ;;
CREATE FUNCTION `divide`(val1 decimal(18,2),val2 decimal(18,2))
 RETURNS decimal(18,2)
BEGIN
	return if((val2=0 or val2 is null),null,convert(val1/val2,decimal(18,2)));
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
