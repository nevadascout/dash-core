using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Dash.Properties;
using FastColoredTextBoxNS;

namespace Dash
{
    public class EditorHelper
    {
        // Styles for Syntax Highlighting
        private readonly TextStyle boldStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);
        private readonly TextStyle blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private readonly TextStyle grayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private readonly TextStyle magentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Bold);
        private readonly TextStyle greenStyle = new TextStyle(Brushes.LimeGreen, null, FontStyle.Regular);
        private readonly TextStyle stringStyle = new TextStyle(Brushes.LightSeaGreen, null, FontStyle.Bold);
        private readonly TextStyle maroonStyle = new TextStyle(Brushes.Navy, null, FontStyle.Regular);

        // Keywords/Snippets for ArmaSense (SQF)
        private readonly string[] keywordList = { "true", "false", "_target", "_x", "_forEachIndex", "_exception", "thisList", "thisTrigger", "_this", "_unit", "abs", "accTime", "acos", "action", "actionKeys", "actionKeysImages", "actionKeysNames", "actionKeysNamesArray", "actionName", "activateAddons", "activatedAddons", "activateKey", "addAction", "addBackpack", "addBackpackCargo", "addBackpackCargoGlobal", "addBackpackGlobal", "addCamShake", "addCuratorAddons", "addCuratorCameraArea", "addCuratorEditableObjects", "addCuratorEditingArea", "addCuratorPoints", "addEditorObject", "addEventHandler", "addGoggles", "addGroupIcon", "addHandgunItem", "addHeadgear", "addItem", "addItemCargo", "addItemCargoGlobal", "addItemPool", "addItemToBackpack", "addItemToUniform", "addItemToVest", "addLiveStats", "addMagazine", "addMagazine array", "addMagazineAmmoCargo", "addMagazineCargo", "addMagazineCargoGlobal", "addMagazineGlobal", "addMagazinePool", "addMagazines", "addMagazineTurret", "addMenu", "addMenuItem", "addMissionEventHandler", "addMPEventHandler", "addMusicEventHandler", "addPrimaryWeaponItem", "addPublicVariableEventHandler", "addRating", "addResources", "addScore", "addScoreSide", "addSecondaryWeaponItem", "addSwitchableUnit", "addTeamMember", "addToRemainsCollector", "addUniform", "addVehicle", "addVest", "addWaypoint", "addWeapon", "addWeaponCargo", "addWeaponCargoGlobal", "addWeaponGlobal", "addWeaponPool", "addWeaponTurret", "agent", "agents", "aimedAtTarget", "aimPos", "airDensityRTD", "airportSide", "AISFinishHeal", "alive", "allControls", "allCurators", "allDead", "allDeadMen", "allDisplays", "allGroups", "allMapMarkers", "allMines", "allMissionObjects", "allow3DMode", "allowCrewInImmobile", "allowCuratorLogicIgnoreAreas", "allowDamage", "allowDammage", "allowFileOperations", "allowFleeing", "allowGetIn", "allSites", "allTurrets", "allUnits", "allUnitsUAV", "allVariables", "ammo", "and", "animate", "animateDoor", "animationPhase", "animationState", "armoryPoints", "asin", "ASLToATL", "assert", "assignAsCargo", "assignAsCargoIndex", "assignAsCommander", "assignAsDriver", "assignAsGunner", "assignAsTurret", "assignCurator", "assignedCargo", "assignedCommander", "assignedDriver", "assignedGunner", "assignedItems", "assignedTarget", "assignedTeam", "assignedVehicle", "assignedVehicleRole", "assignItem", "assignTeam", "assignToAirport", "atan", "atan2", "atg", "ATLToASL", "attachedObject", "attachedObjects", "attachedTo", "attachObject", "attachTo", "attackEnabled", "backpack", "backpackCargo", "backpackContainer", "backpackItems", "backpackMagazines", "backpackSpaceFor", "batteryChargeRTD", "behaviour", "benchmark", "binocular", "blufor", "boundingBox", "boundingBoxReal", "boundingCenter", "breakOut", "breakTo", "briefingName", "buildingExit", "buildingPos", "buldozer LoadNewRoads", "buldozer reloadOperMap", "buttonAction", "buttonSetAction", "cadetMode", "call", "callExtension", "camCommand", "camCommit", "camCommitPrepared", "camCommitted", "camConstuctionSetParams", "camCreate", "camDestroy", "cameraEffect", "cameraEffectEnableHUD", "cameraInterest", "cameraOn", "cameraView", "campaignConfigFile", "camPreload", "camPreloaded", "camPrepareBank", "camPrepareDir", "camPrepareDive", "camPrepareFocus", "camPrepareFov", "camPrepareFovRange", "camPreparePos", "camPrepareRelPos", "camPrepareTarget", "camSetBank", "camSetDir", "camSetDive", "camSetFocus", "camSetFov", "camSetFovRange", "camSetPos", "camSetRelPos", "camSetTarget", "camTarget", "camUseNVG", "canAdd", "canAddItemToBackpack", "canAddItemToUniform", "canAddItemToVest", "cancelSimpleTaskDestination", "canFire", "canMove", "canSlingLoad", "canStand", "canUnloadInCombat", "captive", "captiveNum", "case", "catch", "cbChecked", "cbSetChecked", "ceil", "CfgWeapons drySound", "cheatsEnabled", "checkAIFeature", "civilian", "className", "clearAllItemsFromBackpack", "clearBackpackCargo", "clearBackpackCargoGlobal", "clearGroupIcons", "clearItemCargo", "clearItemCargoGlobal", "clearItemPool", "clearMagazineCargo", "clearMagazineCargoGlobal", "clearMagazinePool", "clearOverlay", "clearRadio", "clearVehicleInit", "clearWeaponCargo", "clearWeaponCargoGlobal", "clearWeaponPool", "closeDialog", "closeDisplay", "closeOverlay", "collapseObjectTree", "collectiveRTD", "combatMode", "commandArtilleryFire", "commandChat", "commander", "commandFire", "commandFollow", "commandFSM", "commandGetOut", "commandingMenu", "commandMove", "commandRadio", "commandStop", "commandTarget", "commandWatch", "comment", "commitOverlay", "compile", "compileFinal", "completedFSM", "composeText", "configClasses", "configFile", "configName", "configProperties", "connectTerminalToUAV", "controlNull", "copyFromClipboard", "copyToClipboard", "copyWaypoints", "cos", "count", "countEnemy", "countFriendly", "countSide", "countType", "countUnknown", "createAgent", "createCenter", "createDialog", "createDiaryLink", "createDiaryRecord", "createDiarySubject", "createDisplay", "createGearDialog", "createGroup", "createGuardedPoint", "createLocation", "createMarker", "createMarkerLocal", "createMenu", "createMine", "createMissionDisplay", "createSimpleTask", "createSite", "createSoundSource", "createTarget", "createTask", "createTeam", "createTrigger", "createUnit", "createUnit array", "createVehicle", "createVehicle array", "createVehicleCrew", "createVehicleLocal", "crew", "ctrlActivate", "ctrlAddEventHandler", "ctrlAutoScrollDelay", "ctrlAutoScrollRewind", "ctrlAutoScrollSpeed", "ctrlChecked", "ctrlClassName", "ctrlCommit", "ctrlCommitted", "ctrlCreate", "ctrlDelete", "ctrlEnable", "ctrlEnabled", "ctrlFade", "ctrlHTMLLoaded", "ctrlIDC", "ctrlIDD", "ctrlMapAnimAdd", "ctrlMapAnimClear", "ctrlMapAnimCommit", "ctrlMapAnimDone", "ctrlMapCursor", "ctrlMapMouseOver", "ctrlMapScale", "ctrlMapScreenToWorld", "ctrlMapWorldToScreen", "ctrlModel", "ctrlModelDirAndUp", "ctrlModelScale", "ctrlParent", "ctrlPosition", "ctrlRemoveAllEventHandlers", "ctrlRemoveEventHandler", "ctrlScale", "ctrlSetActiveColor", "ctrlSetAutoScrollDelay", "ctrlSetAutoScrollRewind", "ctrlSetAutoScrollSpeed", "ctrlSetBackgroundColor", "ctrlSetChecked", "ctrlSetEventHandler", "ctrlSetFade", "ctrlSetFocus", "ctrlSetFont", "ctrlSetFontH1", "ctrlSetFontH1B", "ctrlSetFontH2", "ctrlSetFontH2B", "ctrlSetFontH3", "ctrlSetFontH3B", "ctrlSetFontH4", "ctrlSetFontH4B", "ctrlSetFontH5", "ctrlSetFontH5B", "ctrlSetFontH6", "ctrlSetFontH6B", "ctrlSetFontHeight", "ctrlSetFontHeightH1", "ctrlSetFontHeightH2", "ctrlSetFontHeightH3", "ctrlSetFontHeightH4", "ctrlSetFontHeightH5", "ctrlSetFontHeightH6", "ctrlSetFontP", "ctrlSetFontPB", "ctrlSetForegroundColor", "ctrlSetModel", "ctrlSetModelDirAndUp", "ctrlSetModelScale", "ctrlSetPosition", "ctrlSetScale", "ctrlSetStructuredText", "ctrlSetText", "ctrlSetTextColor", "ctrlSetTooltip", "ctrlSetTooltipColorBox", "ctrlSetTooltipColorShade", "ctrlSetTooltipColorText", "ctrlShow", "ctrlShown", "ctrlText", "ctrlTextHeight", "ctrlType", "ctrlVisible", "curatorAddons", "curatorCamera", "curatorCameraArea", "curatorCameraAreaCeiling", "curatorCoef", "curatorEditableObjects", "curatorEditingArea", "curatorEditingAreaType", "curatorMouseOver", "curatorPoints", "curatorRegisteredObjects", "curatorSelected", "curatorWaypointCost", "currentCommand", "currentMagazine", "currentMagazineDetail", "currentMagazineDetailTurret", "currentMagazineTurret", "currentMuzzle", "currentTask", "currentTasks", "currentVisionMode", "currentWaypoint", "currentWeapon", "currentWeaponMode", "currentWeaponTurret", "currentZeroing", "cursorTarget", "customChat", "customRadio", "cutFadeOut", "cutObj", "cutRsc", "cutText", "damage", "date", "dateToNumber", "daytime", "deActivateKey", "debriefingText", "debugFSM", "debugLog", "default", "deg", "deleteAt", "deleteCenter", "deleteCollection", "deleteEditorObject", "deleteGroup", "deleteIdentity", "deleteLocation", "deleteMarker", "deleteMarkerLocal", "deleteRange", "deleteResources", "deleteSite", "deleteStatus", "deleteTarget", "deleteTeam", "deleteVehicle", "deleteVehicleCrew", "deleteWaypoint", "detach", "detectedMines", "diag_captureFrame", "diag_captureSlowFrame", "diag_fps", "diag_fpsmin", "diag_frameno", "diag_log", "diag_logSlowFrame", "diag_tickTime", "dialog", "diarySubjectExists", "difficulty", "difficultyEnabled", "difficultyEnabledRTD", "direction", "directSay", "disableAI", "disableCollisionWith", "disableConversation", "disableDebriefingStats", "disableSerialization", "disableTIEquipment", "disableUAVConnectability", "disableUserInput", "displayAddEventHandler", "displayCtrl", "displayNull", "displayRemoveAllEventHandlers", "displayRemoveEventHandler", "displaySetEventHandler", "dissolveTeam", "distance", "distanceSqr", "distributionRegion", "do", "doArtilleryFire", "for do", "doFire", "doFollow", "doFSM", "doGetOut", "doMove", "doorPhase", "doStop", "doTarget", "doWatch", "drawArrow", "drawEllipse", "drawIcon", "drawIcon3D", "drawLine", "drawLine3D", "drawLink", "drawLocation", "drawRectangle", "driver", "drop", "drySound CfgWeapons", "east", "echo", "editObject", "editorSetEventHandler", "effectiveCommander", "emptyPositions", "enableAI", "enableAIFeature", "enableAttack", "enableAutoStartUpRTD", "enableAutoTrimRTD", "enableCamShake", "enableCaustics", "enableCollisionWith", "enableCoPilot", "enableCopilot", "enableDebriefingStats", "enableDiagLegend", "enableEndDialog", "enableEngineArtillery", "enableEnvironment", "enableFatigue", "enableGunLights", "enableIRLasers", "enableMimics", "enablePersonTurret", "enableRadio", "enableReload", "enableRopeAttach", "enableSatNormalOnDetail", "enableSaving", "enableSentences", "enableSimulation", "enableSimulationGlobal", "enableTeamSwitch", "enableTraffic", "enableUAVConnectability", "endLoadingScreen", "endMission", "enemy", "engineOn", "enginesIsOnRTD", "enginesRpmRTD", "enginesTorqueRTD", "entities", "estimatedEndServerTime", "estimatedTimeLeft", "evalObjectArgument", "everyBackpack", "everyContainer", "exec", "execEditorScript", "execFSM", "execVM", "exit", "exitWith", "exp", "expectedDestination", "exportLandscapeXYZ", "eyeDirection", "eyePos", "face", "faction", "fadeMusic", "fadeRadio", "fadeSound", "fadeSpeech", "failMission", "", "fillWeaponsFromPool", "find", "findCover", "findDisplay", "findEditorObject", "findEmptyPosition", "findEmptyPositionReady", "findNearestEnemy", "setUnloadInCombat", "finishMissionInit", "finite", "fire", "fireAtTarget", "firstBackpack", "flag", "flagOwner", "fleeing", "floor", "flyInHeight", "fog", "fogForecast", "fogParams", "for forspec", "for var", "forceAddUniform", "forceEnd", "forceMap", "forceRespawn", "forceSpeed", "forceWalk", "forceWeaponFire", "forceWeatherChange", "forEach", "forEachMember", "forEachMemberAgent", "forEachMemberTeam", "format", "formation", "formationDirection", "formationLeader", "formationMembers", "formationPosition", "formationTask", "formatText", "formLeader", "freeLook", "friendly", "from", "fromEditor", "fuel", "fullCrew", "gearSlotAmmoCount", "gearSlotData", "getAmmoCargo", "getArray", "getArtilleryAmmo", "getArtilleryComputerSettings", "getArtilleryETA", "getAssignedCuratorLogic", "getAssignedCuratorUnit", "getBackpackCargo", "getBleedingRemaining", "getBurningValue", "getCargoIndex", "getCenterOfMass", "getChosenCont", "getClientState", "getConnectedUAV", "getDammage", "getDescription", "getDir", "getDirVisual", "getDLCs", "getEditorCamera", "getEditorMode", "getEditorObjectScope", "getElevationOffset", "getFatigue", "getFriend", "getFSMVariable", "getFuelCargo", "getGroupIcon", "getGroupIconParams", "getGroupIcons", "getHideFrom", "getHit", "getHitPointDamage", "getItemCargo", "getMagazineCargo", "getMarkerColor", "getMarkerPos", "getMarkerSize", "getMarkerType", "getMass", "getNumber", "getObjectArgument", "getObjectChildren", "getObjectDLC", "getObjectMaterials", "getObjectProxy", "getObjectTextures", "getOxygenRemaining", "getPersonUsedDLCs", "getPlayerUID", "getPlayerUIDOld", "getPos", "getPosASL", "getPosASLVisual", "getPosASLW", "getPosATL", "getPosATLVisual", "getPosVisual", "getPosWorld", "getRepairCargo", "getResolution", "getShadowDistance", "getSlingLoad", "getSpeed", "getTerrainHeightASL", "getText", "getVariable", "getWeaponCargo", "getWorld", "getWPPos", "glanceAt", "globalChat", "globalRadio", "goggles", "goto", "group", "groupChat", "groupFromNetId", "groupIconSelectable", "groupIconsVisible", "groupID", "groupRadio", "groupSelectedUnits", "groupSelectUnit", "grpNull", "gunner", "gusts", "halt", "handgunItems", "handgunMagazine", "handgunWeapon", "handsHit", "hasInterface", "hasWeapon", "hcAllGroups", "hcGroupParams", "hcLeader", "hcRemoveAllGroups", "hcRemoveGroup", "hcSelected", "hcSelectGroup", "hcSetGroup", "hcShowBar", "hcShownBar", "headgear", "hideBehindScripted", "hideBody", "hideObject", "hideObjectGlobal", "hierarchyObjectsCount", "hint", "hintC", "hintCadet", "hintC array", "hintC structuredText", "hintC text", "hintSilent", "hmd", "hostMission", "htmlLoad", "HUDMovementLevels", "humidity", "image", "importAllGroups", "importance", "in location", "in vehicle", "in Array", "incapacitatedState", "independent", "inflame", "inflamed", "inGameUISetEventHandler", "inheritsFrom", "initAmbientLife", "inputAction", "inRangeOfArtillery", "insertEditorObject", "intersect", "isAbleToBreathe", "isAgent", "isArray", "isAutoHoverOn", "isAutonomous", "isAutotest", "isBleeding", "isBurning", "isClass", "isCollisionLightOn", "isCopilotEnabled", "isDedicated", "isDLCAvailable", "isEngineOn", "isEqualTo", "isFlashlightOn", "isFlatEmpty", "isForcedWalk", "isFormationLeader", "isHidden", "isHideBehindScripted", "isInRemainsCollector", "isInstructorFigureEnabled", "isIRLaserOn", "isKeyActive", "isKindOf", "isLightOn", "isLocalized", "isManualFire", "isMarkedForCollection", "isMultiplayer", "isNil", "isNull", "isNumber", "isObjectRTD", "isOnRoad", "isPiPEnabled", "isPipEnabled", "isPlayer", "isRealTime", "isServer", "isShowing3DIcons", "isSteamMission", "isStreamFriendlyUIEnabled", "isText", "isTouchingGround", "isTutHintsEnabled", "isUAVConnectable", "isUAVConnected", "isUniformAllowed", "isWalking", "itemCargo", "items", "itemsWithMagazines", "join", "joinAs", "joinAsSilent", "joinSilent", "kbAddDatabase", "kbAddDatabaseTargets", "kbAddTopic", "kbHasTopic", "kbReact", "kbRemoveTopic", "kbTell", "kbWasSaid", "keyImage", "keyName", "knowsAbout", "land", "landAt", "landResult", "language", "laserTarget", "lbAdd", "lbClear", "lbColor", "lbCurSel", "lbData", "lbDelete", "lbIsSelected", "lbPicture", "lbSelection", "lbSetColor", "lbSetCurSel", "lbSetData", "lbSetPicture", "lbSetPictureColor", "lbSetPictureColorDisabled", "lbSetPictureColorSelected", "lbSetSelected", "lbSetTooltip", "lbSetValue", "lbSize", "lbSort", "lbSortByValue", "lbText", "lbValue", "leader", "leaveVehicle", "libraryCredits", "libraryDisclaimers", "lifeState", "lightAttachObject", "lightDetachObject", "lightIsOn", "lightnings", "limitSpeed", "linearConversion", "lineBreak", "lineIntersects", "lineIntersectsObjs", "lineIntersectsWith", "linkItem", "list", "listObjects", "ln", "lnbAddArray", "lnbAddColumn", "lnbAddRow", "lnbClear", "lnbColor", "lnbCurSelRow", "lnbData", "lnbDeleteColumn", "lnbDeleteRow", "lnbGetColumnsPosition", "lnbPicture", "lnbSetColor", "lnbSetColumnsPos", "lnbSetCurSelRow", "lnbSetData", "lnbSetPicture", "lnbSetText", "lnbSetValue", "lnbSize", "lnbText", "lnbValue", "load", "loadAbs", "loadBackpack", "loadFile", "loadGame", "loadIdentity", "loadMagazine", "loadOverlay", "loadStatus", "loadUniform", "loadVest", "local", "localize", "locationNull", "locationPosition", "lock", "lockCameraTo", "lockCargo", "lockDriver", "locked", "lockedCargo", "lockedDriver", "lockedTurret", "lockTurret", "lockWP", "log", "logEntities", "lookAt", "lookAtPos", "magazineCargo", "magazines", "magazinesAmmo", "magazinesAmmoCargo", "magazinesAmmoFull", "magazinesDetail", "magazinesDetailBackpack", "magazinesDetailUniform", "magazinesDetailVest", "magazinesTurret", "magazineTurretAmmo", "mapAnimAdd", "mapAnimClear", "mapAnimCommit", "mapAnimDone", "mapCenterOnCamera", "mapGridPosition", "markAsFinishedOnSteam", "markerAlpha", "markerBrush", "markerColor", "markerDir", "markerPos", "markerShape", "markerSize", "markerText", "markerType", "max", "members", "min", "mineActive", "mineDetectedBy", "missionConfigFile", "missionName", "missionNamespace", "missionStart", "mod", "modelToWorld", "modelToWorldVisual", "moonIntensity", "morale", "move", "moveInAny", "moveInCargo", "moveInCommander", "moveInDriver", "moveInGunner", "moveInTurret", "moveObjectToEnd", "moveOut", "moveTarget", "moveTime", "moveTo", "moveToCompleted", "moveToFailed", "musicVolume", "name", "name location", "nameSound", "nearEntities", "nearestBuilding", "nearestLocation", "nearestLocations", "nearestLocationWithDubbing", "nearestObject", "nearestObjects", "nearObjects", "nearObjectsReady", "nearRoads", "nearSupplies", "nearTargets", "needReload", "netId", "netObjNull", "newOverlay", "nextMenuItemIndex", "nextWeatherChange", "nil", "nMenuItems", "not", "numberOfEnginesRTD", "numberToDate", "object", "objectCurators", "objectFromNetId", "objNull", "objStatus", "onBriefingGear", "onBriefingGroup", "onBriefingNotes", "onBriefingPlan", "onBriefingTeamSwitch", "onCommandModeChanged", "onDoubleClick", "onEachFrame", "onGroupIconClick", "onGroupIconOverEnter", "onGroupIconOverLeave", "onHCGroupSelectionChanged", "onMapSingleClick", "onPlayerConnected", "onPlayerDisconnected", "onPreloadFinished", "onPreloadStarted", "onShowNewObject", "onTeamSwitch", "openCuratorInterface", "openDSInterface", "openMap", "openYoutubeVideo", "opfor", "or", "orderGetIn", "overcast", "overcastForecast", "owner", "parseNumber", "parseText", "parsingNamespace", "particlesQuality", "pi", "pickWeaponPool", "pitch", "playableSlotsNumber", "playableUnits", "playAction", "playActionNow", "player", "playerRespawnTime", "playerSide", "playersNumber", "playGesture", "playMission", "playMove", "playMoveNow", "playMusic", "playScriptedMission", "playSound", "playSound3D", "position", "position location", "posScreenToWorld", "positionCameraToWorld", "posWorldToScreen", "ppEffectAdjust", "ppEffectCommit", "ppEffectCommitted", "ppEffectCreate", "ppEffectDestroy", "ppEffectEnable", "ppEffectForceInNVG", "precision", "preloadCamera", "preloadObject", "preloadSound", "preloadTitleObj", "preloadTitleRsc", "preprocessFile", "preprocessFileLineNumbers", "primaryWeapon", "primaryWeaponItems", "primaryWeaponMagazine", "priority", "private", "processDiaryLink", "processInitCommands", "productVersion", "profileName", "profileNamespace", "progressLoadingScreen", "progressPosition", "progressSetPosition", "publicVariable", "publicVariableClient", "publicVariableServer", "pushBack", "putWeaponPool", "queryItemsPool", "queryMagazinePool", "queryWeaponPool", "rad", "radioChannelAdd", "radioChannelCreate", "radioChannelRemove", "radioChannelSetCallSign", "radioChannelSetLabel", "radioVolume", "rain", "rainbow", "random", "rank", "rankId", "rating", "rectangular", "registeredTasks", "registerTask", "reload", "reloadEnabled", "remoteControl", "removeAction", "removeAllActions", "removeAllAssignedItems", "removeAllContainers", "removeAllCuratorAddons", "removeAllCuratorCameraAreas", "removeAllCuratorEditingAreas", "removeAllEventHandlers", "removeAllHandgunItems", "removeAllItems", "removeAllItemsWithMagazines", "removeAllMissionEventHandlers", "removeAllMPEventHandlers", "removeAllMusicEventHandlers", "removeAllPrimaryWeaponItems", "removeAllWeapons", "removeBackpack", "removeBackpackGlobal", "removeClothing", "removeCuratorAddons", "removeCuratorCameraArea", "removeCuratorEditableObjects", "removeCuratorEditingArea", "removeDrawIcon", "removeDrawLinks", "removeEventHandler", "removeFromRemainsCollector", "removeGoggles", "removeGroupIcon", "removeHandgunItem", "removeHeadgear", "removeItem", "removeItemFromBackpack", "removeItemFromUniform", "removeItemFromVest", "removeItems", "removeMagazine", "removeMagazineGlobal", "removeMagazines", "removeMagazinesTurret", "removeMagazineTurret", "removeMenuItem", "removeMissionEventHandler", "removeMPEventHandler", "removeMusicEventHandler", "removePrimaryWeaponItem", "removeSecondaryWeaponItem", "", "removeSimpleTask", "removeSwitchableUnit", "removeTeamMember", "removeUniform", "removeVest", "removeWeapon", "removeWeaponGlobal", "removeWeaponTurret", "requiredVersion", "resetCamShake", "resetSubgroupDirection", "resistance", "resize", "resources", "respawnVehicle", "restartEditorCamera", "reveal", "revealMine", "reverse", "reversedMouseY", "roadsConnectedTo", "ropeAttachedObjects", "ropeAttachedTo", "ropeAttachEnabled", "ropeAttachTo", "ropeCreate", "ropeCut", "ropeDestroy", "ropeDetach", "ropeEndPosition", "ropeLength", "ropes", "ropeSetCargoMass", "ropeUnwind", "ropeUnwound", "rotorsForcesRTD", "rotorsRpmRTD", "round", "runInitScript", "safeZoneH", "safeZoneW", "safeZoneWAbs", "safeZoneX", "safeZoneXAbs", "safeZoneY", "saveGame", "saveIdentity", "saveJoysticks", "saveOverlay", "saveProfileNamespace", "saveStatus", "saveVar", "savingEnabled", "say", "say2D", "say3D", "scopeName", "score", "scoreSide", "screenToWorld", "scriptDone", "scriptName", "scriptNull", "scudState", "secondaryWeapon", "secondaryWeaponItems", "secondaryWeaponMagazine", "select", "selectBestPlaces", "selectDiarySubject", "selectedEditorObjects", "selectEditorObject", "selectionPosition", "selectLeader", "selectNoPlayer", "selectPlayer", "selectWeapon", "selectWeaponTurret", "sendAUMessage", "sendSimpleCommand", "sendTask", "sendTaskResult", "sendUDPMessage", "serverCommand", "serverCommandAvailable", "serverCommandExecutable", "serverTime", "set", "setAccTime", "setActualCollectiveRTD", "setAirportSide", "setAmmo", "setAmmoCargo", "setAperture", "setApertureNew", "setAPURTD", "setArmoryPoints", "setAttributes", "setAutonomous", "setBatteryChargeRTD", "setBatteryRTD", "setBehaviour", "setBleedingRemaining", "setBrakesRTD", "setCameraEffect", "setCameraInterest", "setCamShakeDefParams", "setCamShakeParams", "setCamUseTi", "setCaptive", "setCenterOfMass", "setCollisionLight", "setCombatMode", "setCompassOscillation", "setCuratorCameraAreaCeiling", "setCuratorCoef", "setCuratorEditingAreaType", "setCuratorWaypointCost", "setCurrentTask", "setCurrentWaypoint", "setCustomWeightRTD", "setDamage", "setDammage", "setDate", "setDebriefingText", "setDefaultCamera", "setDestination", "setDetailMapBlendPars", "setDir", "setDirection", "setDrawIcon", "setDropInterval", "setEditorMode", "setEditorObjectScope", "setEffectCondition", "setEngineRPMRTD", "setFace", "setFaceAnimation", "setFatigue", "setFlagOwner", "setFlagSide", "setFlagTexture", "setFog", "setFog array", "setFormation", "setFormationTask", "setFormDir", "setFriend", "setFromEditor", "setFSMVariable", "setFuel", "setFuelCargo", "setGroupIcon", "setGroupIconParams", "setGroupIconsSelectable", "setGroupIconsVisible", "setGroupId", "setGusts", "setHideBehind", "setHit", "setHitPointDamage", "setHorizonParallaxCoef", "setHUDMovementLevels", "setIdentity", "setImportance", "setLeader", "setLightAmbient", "setLightAttenuation", "setLightBrightness", "setLightColor", "setLightDayLight", "setLightFlareMaxDistance", "setLightFlareSize", "setLightIntensity", "setLightnings", "setLightUseFlare", "setLocalWindParams", "setMagazineTurretAmmo", "setMarkerAlpha", "setMarkerAlphaLocal", "setMarkerBrush", "setMarkerBrushLocal", "setMarkerColor", "setMarkerColorLocal", "setMarkerDir", "setMarkerDirLocal", "setMarkerPos", "setMarkerPosLocal", "setMarkerShape", "setMarkerShapeLocal", "setMarkerSize", "setMarkerSizeLocal", "setMarkerText", "setMarkerTextLocal", "setMarkerType", "setMarkerTypeLocal", "setMass", "setMimic", "setMousePosition", "setMusicEffect", "setMusicEventHandler", "setName", "setNameSound", "setObjectArguments", "setObjectMaterial", "setObjectProxy", "setObjectTexture", "setObjectTextureGlobal", "setObjectViewDistance", "setOvercast", "setOwner", "setOxygenRemaining", "setParticleCircle", "setParticleClass", "setParticleFire", "setParticleParams", "setParticleRandom", "setPilotLight", "setPiPEffect", "setPitch", "setPlayable", "setPlayerRespawnTime", "setPos", "setPosASL", "setPosASL2", "setPosASLW", "setPosATL", "setPosition", "setPosWorld", "setRadioMsg", "setRain", "setRainbow", "setRandomLip", "setRank", "setRectangular", "setRepairCargo", "setRotorBrakeRTD", "setShadowDistance", "setSide", "setSimpleTaskDescription", "setSimpleTaskDestination", "setSimpleTaskTarget", "setSimulWeatherLayers", "setSize", "setSkill", "setSkill array", "setSlingLoad", "setSoundEffect", "setSpeaker", "setSpeech", "setSpeedMode", "setStarterRTD", "setStatValue", "setSystemOfUnits", "setTargetAge", "setTaskResult", "setTaskState", "setTerrainGrid", "setText", "setThrottleRTD", "setTimeMultiplier", "setTitleEffect", "setToneMapping", "setToneMappingParams", "setTrafficDensity", "setTrafficDistance", "setTrafficGap", "setTrafficSpeed", "setTriggerActivation", "setTriggerArea", "setTriggerStatements", "setTriggerText", "setTriggerTimeout", "setTriggerType", "setType", "setUnconscious", "setUnitAbility", "setUnitPos", "setUnitPosWeak", "setUnitRank", "setUnitRecoilCoefficient", "setUserActionText", "setVariable", "setVectorDir", "setVectorDirAndUp", "setVectorUp", "setVehicleAmmo", "setVehicleAmmoDef", "setVehicleArmor", "setVehicleId", "setVehicleInit", "setVehicleLock", "setVehiclePosition", "setVehicleTiPars", "setVehicleVarName", "setVelocity", "setVelocityTransformation", "setViewDistance", "setVisibleIfTreeCollapsed", "setWantedRPMRTD", "setWaves", "setWaypointBehaviour", "setWaypointCombatMode", "setWaypointCompletionRadius", "setWaypointDescription", "setWaypointFormation", "setWaypointHousePosition", "setWaypointLoiterRadius", "setWaypointLoiterType", "setWaypointName", "setWaypointPosition", "setWaypointScript", "setWaypointSpeed", "setWaypointStatements", "setWaypointTimeout", "setWaypointType", "setWaypointVisible", "setWeaponReloadingTime", "setWind", "setWindDir", "setWindForce", "setWindStr", "setWPPos", "show3DIcons", "showChat", "showCinemaBorder", "showCommandingMenu", "showCompass", "showCuratorCompass", "showGPS", "showHUD", "showLegend", "showMap", "shownArtilleryComputer", "shownChat", "shownCompass", "shownCuratorCompass", "showNewEditorObject", "shownGPS", "shownMap", "shownPad", "shownRadio", "shownUAVFeed", "shownWarrant", "shownWatch", "showPad", "showRadio", "showSubtitles", "showUAVFeed", "showWarrant", "showWatch", "showWaypoint", "side", "side location", "sideChat", "sideEnemy", "sideFriendly", "sideLogic", "sideRadio", "sideUnknown", "simpleTasks", "simulationEnabled", "simulCloudDensity", "simulCloudOcclusion", "simulInClouds", "simulSetHumidity", "simulWeatherSync", "sin", "size", "sizeOf", "skill", "skillFinal", "skill vehicle", "skipTime", "sleep", "sliderPosition", "sliderRange", "sliderSetPosition", "sliderSetRange", "sliderSetSpeed", "sliderSpeed", "slingLoadAssistantShown", "soldierMagazines", "someAmmo", "soundVolume", "spawn", "speaker", "speed", "speedMode", "sqrt", "squadParams", "stance", "startLoadingScreen", "step", "stop", "stopped", "str", "sunOrMoon", "supportInfo", "suppressFor", "surfaceIsWater", "surfaceNormal", "surfaceType", "swimInDepth", "switch", "switch do", "switchableUnits", "switchAction", "switchCamera", "switchGesture", "switchLight", "switchMove", "synchronizedObjects", "synchronizedTriggers", "synchronizedWaypoints", "synchronizeObjectsAdd", "synchronizeObjectsRemove", "synchronizeTrigger", "synchronizeWaypoint", "synchronizeWaypoint trigger", "systemChat", "systemOfUnits", "tan", "targetsAggregate", "targetsQuery", "taskChildren", "taskCompleted", "taskDescription", "taskDestination", "taskHint", "taskNull", "taskParent", "taskResult", "taskState", "teamMember", "teamMemberNull", "teamName", "teams", "teamSwitch", "teamSwitchEnabled", "teamType", "terminate", "terrainIntersect", "terrainIntersectASL", "text", "text location", "textLog", "textLogFormat", "tg", "throttleRTD", "throw", "time", "timeMultiplier", "titleCut", "titleFadeOut", "titleObj", "titleRsc", "titleText", "to", "toArray", "toLower", "toString", "toUpper", "triggerActivated", "triggerActivation", "triggerArea", "triggerAttachedVehicle", "triggerAttachObject", "triggerAttachVehicle", "triggerStatements", "triggerText", "triggerTimeout", "triggerTimeoutCurrent", "triggerType", "try", "turretLocal", "turretOwner", "turretUnit", "tvAdd", "tvClear", "tvCollapse", "tvCount", "tvCurSel", "tvData", "tvDelete", "tvExpand", "tvPicture", "tvSetCurSel", "tvSetData", "tvSetPicture", "tvSetValue", "tvSort", "tvSortByValue", "tvText", "tvValue", "type", "typeName", "typeOf", "UAVControl", "uiNamespace", "uiSleep", "unassignCurator", "unassignItem", "unassignTeam", "unassignVehicle", "underwater", "uniform", "uniformContainer", "uniformItems", "uniformMagazines", "unitAddons", "unitBackpack", "unitPos", "unitReady", "unitRecoilCoefficient", "units", "unitsBelowHeight", "unlinkItem", "unlockAchievement", "unregisterTask", "updateDrawIcon", "updateMenuItem", "updateObjectTree", "useAudioTimeForMoves", "vectorAdd", "vectorCos", "vectorCrossProduct", "vectorDiff", "vectorDir", "vectorDirVisual", "vectorDistance", "vectorDistanceSqr", "vectorDotProduct", "vectorFromTo", "vectorMagnitude", "vectorMagnitudeSqr", "vectorMultiply", "vectorNormalized", "vectorUp", "vectorUpVisual", "vehicle", "vehicleChat", "vehicleRadio", "vehicles", "vehicleVarName", "velocity", "velocityModelSpace", "verifySignature", "vest", "vestContainer", "vestItems", "vestMagazines", "viewDistance", "visibleCompass", "visibleGPS", "visibleMap", "visiblePosition", "visiblePositionASL", "visibleWatch", "waitUntil", "waves", "waypointAttachedObject", "waypointAttachedVehicle", "waypointAttachObject", "waypointAttachVehicle", "waypointBehaviour", "waypointCombatMode", "waypointCompletionRadius", "waypointDescription", "waypointFormation", "waypointHousePosition", "waypointLoiterRadius", "waypointLoiterType", "waypointName", "waypointPosition", "waypoints", "waypointScript", "waypointShow", "waypointSpeed", "waypointStatements", "waypointTimeout", "waypointTimeoutCurrent", "waypointType", "waypointVisible", "weaponAccessories", "weaponCargo", "weaponDirection", "weaponLowered", "weapons", "weaponsItems", "weaponsItemsCargo", "weaponState", "weaponsTurret", "weightRTD", "west", "WFSideText", "while", "wind", "windDir", "windStr", "with", "worldName", "worldToModel", "worldToModelVisual", "worldToScreen" };
        private string[] snippets = { }; //{ "diag_log \"\";", "for \"_i\" from 1 to 10 do { debugLog _i; };", "call compile preprocessFileLine Numbers \"\";" };
        private string[] userVariables = { };

        public List<UserVariable> UserVariablesCurrentFile { get; set; }
        public int ArmaListItemsCount { get; set; }

        private EventHandler<TextChangedEventArgs> TextAreaTextChanged { get; set; }
        private EventHandler<TextChangedEventArgs> TextAreaTextChangedDelayed { get; set; }
        private KeyEventHandler TextAreaKeyUp { get; set; }
        private EventHandler TextAreaSelectionChangedDelayed { get; set; }
        private DragEventHandler TextAreaDragDrop { get; set; }
        private DragEventHandler TextAreaDragEnter { get; set; }
        public TabControl MainTabControl { get; set; }
        public ImageList ArmaSenseImageList { get; set; }

        public AutocompleteMenu ArmaSense { get; set; }
        private List<AutocompleteItem> ArmaSenseKeywords { get; set; }

        public FastColoredTextBox ActiveEditor
        {
            get { return GetActiveEditor(); }
        }

        public EditorHelper(
            EventHandler<TextChangedEventArgs> textAreaTextChanged,
            EventHandler<TextChangedEventArgs> textAreaTextChangedDelayed,
            KeyEventHandler textAreaKeyUp,
            EventHandler textAreaSelectionChangedDelayed,
            DragEventHandler textAreaDragDrop,
            DragEventHandler textAreaDragEnter,
            TabControl mainTabControl,
            AutocompleteMenu armaSense)
        {
            TextAreaTextChanged = textAreaTextChanged;
            TextAreaTextChangedDelayed = textAreaTextChangedDelayed;
            TextAreaKeyUp = textAreaKeyUp;
            TextAreaSelectionChangedDelayed = textAreaSelectionChangedDelayed;
            TextAreaDragDrop = textAreaDragDrop;
            TextAreaDragEnter = textAreaDragEnter;
            MainTabControl = mainTabControl;
            ArmaSense = armaSense;

            ArmaListItemsCount = 0;
            ArmaSenseKeywords = new List<AutocompleteItem>();

            foreach (var item in keywordList)
            {
                ArmaSenseKeywords.Add(new AutocompleteItem(item) { ImageIndex = 0, ToolTipTitle = "Arma Script Command", ToolTipText = item });
            }
        }



        public FastColoredTextBox CreateEditor(string filePath = "")
        {
            var fctb = new FastColoredTextBox
            {
                Dock = DockStyle.Fill,

                AutoIndent = true,
                AutoIndentChars = true,

                AllowDrop = true,

                AllowMacroRecording = false,
                AutoCompleteBrackets = true,
                AutoCompleteBracketsList = new char[] { '{', '}', '(', ')', '[', ']', '"', '"', '\'', '\'' },
                AutoIndentExistingLines = false,
                CaretColor = Color.Navy,
                CurrentLineColor = Color.CadetBlue,
                LeftPadding = 15,
                LineInterval = 3,
                LineNumberColor = Color.DimGray,
                BookmarkColor = Color.Olive,

                FindEndOfFoldingBlockStrategy = FindEndOfFoldingBlockStrategy.Strategy2,

                DelayedEventsInterval = 100,

                SelectionColor = Color.FromArgb(80, 0, 128, 128),
                ServiceLinesColor = Color.DarkGray,
                ShowFoldingLines = true,

                Tag = filePath
            };

            if (Settings.Default.ShowRuler)
            {
                fctb.PreferredLineWidth = Settings.Default.RulerWidth;
            }

            fctb.TextChanged += TextAreaTextChanged;
            fctb.TextChangedDelayed += TextAreaTextChangedDelayed;
            fctb.KeyUp += TextAreaKeyUp;
            fctb.SelectionChangedDelayed += TextAreaSelectionChangedDelayed;
            fctb.DragDrop += TextAreaDragDrop;
            fctb.DragEnter += TextAreaDragEnter;

            //fctb.OpenFile(filePath);

            return fctb;
        }

        public FastColoredTextBox GetActiveEditor()
        {
            return MainTabControl.SelectedTab.Controls[0] as FastColoredTextBox;
        }

        public FastColoredTextBox GetEditorByTab(TabPage tab)
        {
            var tabIndex = MainTabControl.TabPages.IndexOf(tab);
            return MainTabControl.SelectedTab.Controls[tabIndex] as FastColoredTextBox;
        }

        public void SqfHighlight(FastColoredTextBox editor, TextChangedEventArgs e, bool forceRefresh = false)
        {
            editor.LeftBracket = '(';
            editor.RightBracket = ')';
            editor.LeftBracket2 = '[';
            editor.RightBracket2 = ']';

            var range = forceRefresh ? editor.Range : e.ChangedRange;

            //clear style of changed range
            range.ClearStyle(blueStyle, boldStyle, grayStyle, magentaStyle, greenStyle, stringStyle, maroonStyle);


            //comment highlighting
            range.SetStyle(greenStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(greenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(greenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);

            //string highlighting
            range.SetStyle(stringStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");

            //number highlighting
            range.SetStyle(magentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");

            //attribute highlighting
            //range.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);

            //class name highlighting
            //range.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");

            //keyword highlighting
            //range.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            // Clear styling inside comments
            //foreach (Range r in editor.GetRanges(@"^\s*//.*$", RegexOptions.Multiline))
           // {
           //     r.ClearStyle(StyleIndex.All);
            //    r.SetStyle(greenStyle);
           // }

            // SQF Specific Keywords
            range.SetStyle(blueStyle, @"\b(true|false|null|if|then|else|this|while|for|do|foreach|boolean)\b");
            range.SetStyle(maroonStyle, @"\b(abs|accTime|acos|action|actionKeys|actionKeysImages|actionKeysNames|actionKeysNamesArray|actionName|activateAddons|activatedAddons|activateKey|addAction|addBackpack|addBackpackCargo|addBackpackCargoGlobal|addBackpackGlobal|addCamShake|addCuratorAddons|addCuratorCameraArea|addCuratorEditableObjects|addCuratorEditingArea|addCuratorPoints|addEditorObject|addEventHandler|addGoggles|addGroupIcon|addHandgunItem|addHeadgear|addItem|addItemCargo|addItemCargoGlobal|addItemPool|addItemToBackpack|addItemToUniform|addItemToVest|addLiveStats|addMagazine|addMagazine array|addMagazineAmmoCargo|addMagazineCargo|addMagazineCargoGlobal|addMagazineGlobal|addMagazinePool|addMagazines|addMagazineTurret|addMenu|addMenuItem|addMissionEventHandler|addMPEventHandler|addMusicEventHandler|addPrimaryWeaponItem|addPublicVariableEventHandler|addRating|addResources|addScore|addScoreSide|addSecondaryWeaponItem|addSwitchableUnit|addTeamMember|addToRemainsCollector|addUniform|addVehicle|addVest|addWaypoint|addWeapon|addWeaponCargo|addWeaponCargoGlobal|addWeaponGlobal|addWeaponPool|addWeaponTurret|agent|agents|aimedAtTarget|aimPos|airDensityRTD|airportSide|AISFinishHeal|alive|allControls|allCurators|allDead|allDeadMen|allDisplays|allGroups|allMapMarkers|allMines|allMissionObjects|allow3DMode|allowCrewInImmobile|allowCuratorLogicIgnoreAreas|allowDamage|allowDammage|allowFileOperations|allowFleeing|allowGetIn|allSites|allTurrets|allUnits|allUnitsUAV|allVariables|ammo|and|animate|animateDoor|animationPhase|animationState|armoryPoints|asin|ASLToATL|assert|assignAsCargo|assignAsCargoIndex|assignAsCommander|assignAsDriver|assignAsGunner|assignAsTurret|assignCurator|assignedCargo|assignedCommander|assignedDriver|assignedGunner|assignedItems|assignedTarget|assignedTeam|assignedVehicle|assignedVehicleRole|assignItem|assignTeam|assignToAirport|atan|atan2|atg|ATLToASL|attachedObject|attachedObjects|attachedTo|attachObject|attachTo|attackEnabled|backpack|backpackCargo|backpackContainer|backpackItems|backpackMagazines|backpackSpaceFor|batteryChargeRTD|behaviour|benchmark|binocular|blufor|boundingBox|boundingBoxReal|boundingCenter|breakOut|breakTo|briefingName|buildingExit|buildingPos|buldozer LoadNewRoads|buldozer reloadOperMap|buttonAction|buttonSetAction|cadetMode|call|callExtension|camCommand|camCommit|camCommitPrepared|camCommitted|camConstuctionSetParams|camCreate|camDestroy|cameraEffect|cameraEffectEnableHUD|cameraInterest|cameraOn|cameraView|campaignConfigFile|camPreload|camPreloaded|camPrepareBank|camPrepareDir|camPrepareDive|camPrepareFocus|camPrepareFov|camPrepareFovRange|camPreparePos|camPrepareRelPos|camPrepareTarget|camSetBank|camSetDir|camSetDive|camSetFocus|camSetFov|camSetFovRange|camSetPos|camSetRelPos|camSetTarget|camTarget|camUseNVG|canAdd|canAddItemToBackpack|canAddItemToUniform|canAddItemToVest|cancelSimpleTaskDestination|canFire|canMove|canSlingLoad|canStand|canUnloadInCombat|captive|captiveNum|case|catch|cbChecked|cbSetChecked|ceil|CfgWeapons drySound|cheatsEnabled|checkAIFeature|civilian|className|clearAllItemsFromBackpack|clearBackpackCargo|clearBackpackCargoGlobal|clearGroupIcons|clearItemCargo|clearItemCargoGlobal|clearItemPool|clearMagazineCargo|clearMagazineCargoGlobal|clearMagazinePool|clearOverlay|clearRadio|clearVehicleInit|clearWeaponCargo|clearWeaponCargoGlobal|clearWeaponPool|closeDialog|closeDisplay|closeOverlay|collapseObjectTree|collectiveRTD|combatMode|commandArtilleryFire|commandChat|commander|commandFire|commandFollow|commandFSM|commandGetOut|commandingMenu|commandMove|commandRadio|commandStop|commandTarget|commandWatch|comment|commitOverlay|compile|compileFinal|completedFSM|composeText|configClasses|configFile|configName|configProperties|connectTerminalToUAV|controlNull|copyFromClipboard|copyToClipboard|copyWaypoints|cos|count)\b");
            range.SetStyle(maroonStyle, @"\b(countEnemy|countFriendly|countSide|countType|countUnknown|createAgent|createCenter|createDialog|createDiaryLink|createDiaryRecord|createDiarySubject|createDisplay|createGearDialog|createGroup|createGuardedPoint|createLocation|createMarker|createMarkerLocal|createMenu|createMine|createMissionDisplay|createSimpleTask|createSite|createSoundSource|createTarget|createTask|createTeam|createTrigger|createUnit|createUnit array|createVehicle|createVehicle array|createVehicleCrew|createVehicleLocal|crew|ctrlActivate|ctrlAddEventHandler|ctrlAutoScrollDelay|ctrlAutoScrollRewind|ctrlAutoScrollSpeed|ctrlChecked|ctrlClassName|ctrlCommit|ctrlCommitted|ctrlCreate|ctrlDelete|ctrlEnable|ctrlEnabled|ctrlFade|ctrlHTMLLoaded|ctrlIDC|ctrlIDD|ctrlMapAnimAdd|ctrlMapAnimClear|ctrlMapAnimCommit|ctrlMapAnimDone|ctrlMapCursor|ctrlMapMouseOver|ctrlMapScale|ctrlMapScreenToWorld|ctrlMapWorldToScreen|ctrlModel|ctrlModelDirAndUp|ctrlModelScale|ctrlParent|ctrlPosition|ctrlRemoveAllEventHandlers|ctrlRemoveEventHandler|ctrlScale|ctrlSetActiveColor|ctrlSetAutoScrollDelay|ctrlSetAutoScrollRewind|ctrlSetAutoScrollSpeed|ctrlSetBackgroundColor|ctrlSetChecked|ctrlSetEventHandler|ctrlSetFade|ctrlSetFocus|ctrlSetFont|ctrlSetFontH1|ctrlSetFontH1B|ctrlSetFontH2|ctrlSetFontH2B|ctrlSetFontH3|ctrlSetFontH3B|ctrlSetFontH4|ctrlSetFontH4B|ctrlSetFontH5|ctrlSetFontH5B|ctrlSetFontH6|ctrlSetFontH6B|ctrlSetFontHeight|ctrlSetFontHeightH1|ctrlSetFontHeightH2|ctrlSetFontHeightH3|ctrlSetFontHeightH4|ctrlSetFontHeightH5|ctrlSetFontHeightH6|ctrlSetFontP|ctrlSetFontPB|ctrlSetForegroundColor|ctrlSetModel|ctrlSetModelDirAndUp|ctrlSetModelScale|ctrlSetPosition|ctrlSetScale|ctrlSetStructuredText|ctrlSetText|ctrlSetTextColor|ctrlSetTooltip|ctrlSetTooltipColorBox|ctrlSetTooltipColorShade|ctrlSetTooltipColorText|ctrlShow|ctrlShown|ctrlText|ctrlTextHeight|ctrlType|ctrlVisible|curatorAddons|curatorCamera|curatorCameraArea|curatorCameraAreaCeiling|curatorCoef|curatorEditableObjects|curatorEditingArea|curatorEditingAreaType|curatorMouseOver|curatorPoints|curatorRegisteredObjects|curatorSelected|curatorWaypointCost|currentCommand|currentMagazine|currentMagazineDetail|currentMagazineDetailTurret|currentMagazineTurret|currentMuzzle|currentTask|currentTasks|currentVisionMode|currentWaypoint|currentWeapon|currentWeaponMode|currentWeaponTurret|currentZeroing|cursorTarget|customChat|customRadio|cutFadeOut|cutObj|cutRsc|cutText|damage|date|dateToNumber|daytime|deActivateKey|debriefingText|debugFSM|debugLog|default|deg|deleteAt|deleteCenter|deleteCollection|deleteEditorObject|deleteGroup|deleteIdentity|deleteLocation|deleteMarker|deleteMarkerLocal|deleteRange|deleteResources|deleteSite|deleteStatus|deleteTarget|deleteTeam|deleteVehicle|deleteVehicleCrew|deleteWaypoint|detach|detectedMines|diag captureFrame|diag captureSlowFrame|diag fps|diag fpsmin|diag frameno|diag log|diag logSlowFrame|diag tickTime|dialog|diarySubjectExists|difficulty|difficultyEnabled|difficultyEnabledRTD|direction|directSay|disableAI|disableCollisionWith|disableConversation|disableDebriefingStats|disableSerialization|disableTIEquipment|disableUAVConnectability|disableUserInput|displayAddEventHandler|displayCtrl|displayNull|displayRemoveAllEventHandlers|displayRemoveEventHandler|displaySetEventHandler|dissolveTeam|distance|distanceSqr|distributionRegion|do|doArtilleryFire|for do|doFire|doFollow|doFSM|doGetOut|doMove|doorPhase|doStop|doTarget|doWatch|drawArrow|drawEllipse|drawIcon|drawIcon3D|drawLine|drawLine3D|drawLink|drawLocation|drawRectangle|driver|drop|drySound CfgWeapons|east|echo|editObject|editorSetEventHandler|effectiveCommander|emptyPositions|enableAI|enableAIFeature|enableAttack|enableAutoStartUpRTD|enableAutoTrimRTD|enableCamShake|enableCaustics|enableCollisionWith|enableCoPilot|enableCopilot|enableDebriefingStats)\b");
            range.SetStyle(maroonStyle, @"\b(enableDiagLegend|enableEndDialog|enableEngineArtillery|enableEnvironment|enableFatigue|enableGunLights|enableIRLasers|enableMimics|enablePersonTurret|enableRadio|enableReload|enableRopeAttach|enableSatNormalOnDetail|enableSaving|enableSentences|enableSimulation|enableSimulationGlobal|enableTeamSwitch|enableTraffic|enableUAVConnectability|endLoadingScreen|endMission|enemy|engineOn|enginesIsOnRTD|enginesRpmRTD|enginesTorqueRTD|entities|estimatedEndServerTime|estimatedTimeLeft|evalObjectArgument|everyBackpack|everyContainer|exec|execEditorScript|execFSM|execVM|exit|exitWith|exp|expectedDestination|exportLandscapeXYZ|eyeDirection|eyePos|face|faction|fadeMusic|fadeRadio|fadeSound|fadeSpeech|failMission||fillWeaponsFromPool|find|findCover|findDisplay|findEditorObject|findEmptyPosition|findEmptyPositionReady|findNearestEnemy|setUnloadInCombat|finishMissionInit|finite|fire|fireAtTarget|firstBackpack|flag|flagOwner|fleeing|floor|flyInHeight|fog|fogForecast|fogParams|for forspec|for var|forceAddUniform|forceEnd|forceMap|forceRespawn|forceSpeed|forceWalk|forceWeaponFire|forceWeatherChange|forEach|forEachMember|forEachMemberAgent|forEachMemberTeam|format|formation|formationDirection|formationLeader|formationMembers|formationPosition|formationTask|formatText|formLeader|freeLook|friendly|from|fromEditor|fuel|fullCrew|gearSlotAmmoCount|gearSlotData|getAmmoCargo|getArray|getArtilleryAmmo|getArtilleryComputerSettings|getArtilleryETA|getAssignedCuratorLogic|getAssignedCuratorUnit|getBackpackCargo|getBleedingRemaining|getBurningValue|getCargoIndex|getCenterOfMass|getChosenCont|getClientState|getConnectedUAV|getDammage|getDescription|getDir|getDirVisual|getDLCs|getEditorCamera|getEditorMode|getEditorObjectScope|getElevationOffset|getFatigue|getFriend|getFSMVariable|getFuelCargo|getGroupIcon|getGroupIconParams|getGroupIcons|getHideFrom|getHit|getHitPointDamage|getItemCargo|getMagazineCargo|getMarkerColor|getMarkerPos|getMarkerSize|getMarkerType|getMass|getNumber|getObjectArgument|getObjectChildren|getObjectDLC|getObjectMaterials|getObjectProxy|getObjectTextures|getOxygenRemaining|getPersonUsedDLCs|getPlayerUID|getPlayerUIDOld|getPos|getPosASL|getPosASLVisual|getPosASLW|getPosATL|getPosATLVisual|getPosVisual|getPosWorld|getRepairCargo|getResolution|getShadowDistance|getSlingLoad|getSpeed|getTerrainHeightASL|getText|getVariable|getWeaponCargo|getWorld|getWPPos|glanceAt|globalChat|globalRadio|goggles|goto|group|groupChat|groupFromNetId|groupIconSelectable|groupIconsVisible|groupID|groupRadio|groupSelectedUnits|groupSelectUnit|grpNull|gunner|gusts|halt|handgunItems|handgunMagazine|handgunWeapon|handsHit|hasInterface|hasWeapon|hcAllGroups|hcGroupParams|hcLeader|hcRemoveAllGroups|hcRemoveGroup|hcSelected|hcSelectGroup|hcSetGroup|hcShowBar|hcShownBar|headgear|hideBehindScripted|hideBody|hideObject|hideObjectGlobal|hierarchyObjectsCount|hint|hintC|hintCadet|hintC array|hintC structuredText|hintC text|hintSilent|hmd|hostMission|htmlLoad|HUDMovementLevels|humidity|image|importAllGroups|importance|in location|in vehicle|in Array|incapacitatedState|independent|inflame|inflamed|inGameUISetEventHandler|inheritsFrom|initAmbientLife|inputAction|inRangeOfArtillery|insertEditorObject|intersect|isAbleToBreathe|isAgent|isArray|isAutoHoverOn|isAutonomous|isAutotest|isBleeding|isBurning|isClass|isCollisionLightOn|isCopilotEnabled|isDedicated|isDLCAvailable|isEngineOn|isEqualTo|isFlashlightOn|isFlatEmpty|isForcedWalk|isFormationLeader|isHidden|isHideBehindScripted|isInRemainsCollector|isInstructorFigureEnabled|isIRLaserOn|isKeyActive|isKindOf|isLightOn|isLocalized|isManualFire|isMarkedForCollection|isMultiplayer|isNil|isNull|isNumber|isObjectRTD|isOnRoad|isPiPEnabled|isPipEnabled|isPlayer|isRealTime|isServer|isShowing3DIcons|isSteamMission|isStreamFriendlyUIEnabled)\b");
            range.SetStyle(maroonStyle, @"\b(isText|isTouchingGround|isTutHintsEnabled|isUAVConnectable|isUAVConnected|isUniformAllowed|isWalking|itemCargo|items|itemsWithMagazines|join|joinAs|joinAsSilent|joinSilent|kbAddDatabase|kbAddDatabaseTargets|kbAddTopic|kbHasTopic|kbReact|kbRemoveTopic|kbTell|kbWasSaid|keyImage|keyName|knowsAbout|land|landAt|landResult|language|laserTarget|lbAdd|lbClear|lbColor|lbCurSel|lbData|lbDelete|lbIsSelected|lbPicture|lbSelection|lbSetColor|lbSetCurSel|lbSetData|lbSetPicture|lbSetPictureColor|lbSetPictureColorDisabled|lbSetPictureColorSelected|lbSetSelected|lbSetTooltip|lbSetValue|lbSize|lbSort|lbSortByValue|lbText|lbValue|leader|leaveVehicle|libraryCredits|libraryDisclaimers|lifeState|lightAttachObject|lightDetachObject|lightIsOn|lightnings|limitSpeed|linearConversion|lineBreak|lineIntersects|lineIntersectsObjs|lineIntersectsWith|linkItem|list|listObjects|ln|lnbAddArray|lnbAddColumn|lnbAddRow|lnbClear|lnbColor|lnbCurSelRow|lnbData|lnbDeleteColumn|lnbDeleteRow|lnbGetColumnsPosition|lnbPicture|lnbSetColor|lnbSetColumnsPos|lnbSetCurSelRow|lnbSetData|lnbSetPicture|lnbSetText|lnbSetValue|lnbSize|lnbText|lnbValue|load|loadAbs|loadBackpack|loadFile|loadGame|loadIdentity|loadMagazine|loadOverlay|loadStatus|loadUniform|loadVest|local|localize|locationNull|locationPosition|lock|lockCameraTo|lockCargo|lockDriver|locked|lockedCargo|lockedDriver|lockedTurret|lockTurret|lockWP|log|logEntities|lookAt|lookAtPos|magazineCargo|magazines|magazinesAmmo|magazinesAmmoCargo|magazinesAmmoFull|magazinesDetail|magazinesDetailBackpack|magazinesDetailUniform|magazinesDetailVest|magazinesTurret|magazineTurretAmmo|mapAnimAdd|mapAnimClear|mapAnimCommit|mapAnimDone|mapCenterOnCamera|mapGridPosition|markAsFinishedOnSteam|markerAlpha|markerBrush|markerColor|markerDir|markerPos|markerShape|markerSize|markerText|markerType|max|members|min|mineActive|mineDetectedBy|missionConfigFile|missionName|missionNamespace|missionStart|mod|modelToWorld|modelToWorldVisual|moonIntensity|morale|move|moveInAny|moveInCargo|moveInCommander|moveInDriver|moveInGunner|moveInTurret|moveObjectToEnd|moveOut|moveTarget|moveTime|moveTo|moveToCompleted|moveToFailed|musicVolume|name|name location|nameSound|nearEntities|nearestBuilding|nearestLocation|nearestLocations|nearestLocationWithDubbing|nearestObject|nearestObjects|nearObjects|nearObjectsReady|nearRoads|nearSupplies|nearTargets|needReload|netId|netObjNull|newOverlay|nextMenuItemIndex|nextWeatherChange|nil|nMenuItems|not|numberOfEnginesRTD|numberToDate|object|objectCurators|objectFromNetId|objNull|objStatus|onBriefingGear|onBriefingGroup|onBriefingNotes|onBriefingPlan|onBriefingTeamSwitch|onCommandModeChanged|onDoubleClick|onEachFrame|onGroupIconClick|onGroupIconOverEnter|onGroupIconOverLeave|onHCGroupSelectionChanged|onMapSingleClick|onPlayerConnected|onPlayerDisconnected|onPreloadFinished|onPreloadStarted|onShowNewObject|onTeamSwitch|openCuratorInterface|openDSInterface|openMap|openYoutubeVideo|opfor|or|orderGetIn|overcast|overcastForecast|owner|parseNumber|parseText|parsingNamespace|particlesQuality|pi|pickWeaponPool|pitch|playableSlotsNumber|playableUnits|playAction|playActionNow|player|playerRespawnTime|playerSide|playersNumber|playGesture|playMission|playMove|playMoveNow|playMusic|playScriptedMission|playSound|playSound3D|position|position location|posScreenToWorld|positionCameraToWorld|posWorldToScreen|ppEffectAdjust|ppEffectCommit|ppEffectCommitted|ppEffectCreate|ppEffectDestroy|ppEffectEnable|ppEffectForceInNVG|precision|preloadCamera|preloadObject|preloadSound|preloadTitleObj|preloadTitleRsc|preprocessFile|preprocessFileLineNumbers|primaryWeapon|primaryWeaponItems|primaryWeaponMagazine|priority|private|processDiaryLink|processInitCommands|productVersion|profileName|profileNamespace|progressLoadingScreen|progressPosition|progressSetPosition)\b");
            range.SetStyle(maroonStyle, @"\b(publicVariable|publicVariableClient|publicVariableServer|pushBack|putWeaponPool|queryItemsPool|queryMagazinePool|queryWeaponPool|rad|radioChannelAdd|radioChannelCreate|radioChannelRemove|radioChannelSetCallSign|radioChannelSetLabel|radioVolume|rain|rainbow|random|rank|rankId|rating|rectangular|registeredTasks|registerTask|reload|reloadEnabled|remoteControl|removeAction|removeAllActions|removeAllAssignedItems|removeAllContainers|removeAllCuratorAddons|removeAllCuratorCameraAreas|removeAllCuratorEditingAreas|removeAllEventHandlers|removeAllHandgunItems|removeAllItems|removeAllItemsWithMagazines|removeAllMissionEventHandlers|removeAllMPEventHandlers|removeAllMusicEventHandlers|removeAllPrimaryWeaponItems|removeAllWeapons|removeBackpack|removeBackpackGlobal|removeClothing|removeCuratorAddons|removeCuratorCameraArea|removeCuratorEditableObjects|removeCuratorEditingArea|removeDrawIcon|removeDrawLinks|removeEventHandler|removeFromRemainsCollector|removeGoggles|removeGroupIcon|removeHandgunItem|removeHeadgear|removeItem|removeItemFromBackpack|removeItemFromUniform|removeItemFromVest|removeItems|removeMagazine|removeMagazineGlobal|removeMagazines|removeMagazinesTurret|removeMagazineTurret|removeMenuItem|removeMissionEventHandler|removeMPEventHandler|removeMusicEventHandler|removePrimaryWeaponItem|removeSecondaryWeaponItem||removeSimpleTask|removeSwitchableUnit|removeTeamMember|removeUniform|removeVest|removeWeapon|removeWeaponGlobal|removeWeaponTurret|requiredVersion|resetCamShake|resetSubgroupDirection|resistance|resize|resources|respawnVehicle|restartEditorCamera|reveal|revealMine|reverse|reversedMouseY|roadsConnectedTo|ropeAttachedObjects|ropeAttachedTo|ropeAttachEnabled|ropeAttachTo|ropeCreate|ropeCut|ropeDestroy|ropeDetach|ropeEndPosition|ropeLength|ropes|ropeSetCargoMass|ropeUnwind|ropeUnwound|rotorsForcesRTD|rotorsRpmRTD|round|runInitScript|safeZoneH|safeZoneW|safeZoneWAbs|safeZoneX|safeZoneXAbs|safeZoneY|saveGame|saveIdentity|saveJoysticks|saveOverlay|saveProfileNamespace|saveStatus|saveVar|savingEnabled|say|say2D|say3D|scopeName|score|scoreSide|screenToWorld|scriptDone|scriptName|scriptNull|scudState|secondaryWeapon|secondaryWeaponItems|secondaryWeaponMagazine|select|selectBestPlaces|selectDiarySubject|selectedEditorObjects|selectEditorObject|selectionPosition|selectLeader|selectNoPlayer|selectPlayer|selectWeapon|selectWeaponTurret|sendAUMessage|sendSimpleCommand|sendTask|sendTaskResult|sendUDPMessage|serverCommand|serverCommandAvailable|serverCommandExecutable|serverTime|set|setAccTime|setActualCollectiveRTD|setAirportSide|setAmmo|setAmmoCargo|setAperture|setApertureNew|setAPURTD|setArmoryPoints|setAttributes|setAutonomous|setBatteryChargeRTD|setBatteryRTD|setBehaviour|setBleedingRemaining|setBrakesRTD|setCameraEffect|setCameraInterest|setCamShakeDefParams|setCamShakeParams|setCamUseTi|setCaptive|setCenterOfMass|setCollisionLight|setCombatMode|setCompassOscillation|setCuratorCameraAreaCeiling|setCuratorCoef|setCuratorEditingAreaType|setCuratorWaypointCost|setCurrentTask|setCurrentWaypoint|setCustomWeightRTD|setDamage|setDammage|setDate|setDebriefingText|setDefaultCamera|setDestination|setDetailMapBlendPars|setDir|setDirection|setDrawIcon|setDropInterval|setEditorMode|setEditorObjectScope|setEffectCondition|setEngineRPMRTD|setFace|setFaceAnimation|setFatigue|setFlagOwner|setFlagSide|setFlagTexture|setFog|setFog array|setFormation|setFormationTask|setFormDir|setFriend|setFromEditor|setFSMVariable|setFuel|setFuelCargo|setGroupIcon|setGroupIconParams|setGroupIconsSelectable|setGroupIconsVisible|setGroupId|setGusts|setHideBehind|setHit|setHitPointDamage|setHorizonParallaxCoef|setHUDMovementLevels|setIdentity|setImportance|setLeader|setLightAmbient|setLightAttenuation|setLightBrightness|setLightColor|setLightDayLight|setLightFlareMaxDistance)\b");
            range.SetStyle(maroonStyle, @"\b(setLightFlareSize|setLightIntensity|setLightnings|setLightUseFlare|setLocalWindParams|setMagazineTurretAmmo|setMarkerAlpha|setMarkerAlphaLocal|setMarkerBrush|setMarkerBrushLocal|setMarkerColor|setMarkerColorLocal|setMarkerDir|setMarkerDirLocal|setMarkerPos|setMarkerPosLocal|setMarkerShape|setMarkerShapeLocal|setMarkerSize|setMarkerSizeLocal|setMarkerText|setMarkerTextLocal|setMarkerType|setMarkerTypeLocal|setMass|setMimic|setMousePosition|setMusicEffect|setMusicEventHandler|setName|setNameSound|setObjectArguments|setObjectMaterial|setObjectProxy|setObjectTexture|setObjectTextureGlobal|setObjectViewDistance|setOvercast|setOwner|setOxygenRemaining|setParticleCircle|setParticleClass|setParticleFire|setParticleParams|setParticleRandom|setPilotLight|setPiPEffect|setPitch|setPlayable|setPlayerRespawnTime|setPos|setPosASL|setPosASL2|setPosASLW|setPosATL|setPosition|setPosWorld|setRadioMsg|setRain|setRainbow|setRandomLip|setRank|setRectangular|setRepairCargo|setRotorBrakeRTD|setShadowDistance|setSide|setSimpleTaskDescription|setSimpleTaskDestination|setSimpleTaskTarget|setSimulWeatherLayers|setSize|setSkill|setSkill array|setSlingLoad|setSoundEffect|setSpeaker|setSpeech|setSpeedMode|setStarterRTD|setStatValue|setSystemOfUnits|setTargetAge|setTaskResult|setTaskState|setTerrainGrid|setText|setThrottleRTD|setTimeMultiplier|setTitleEffect|setToneMapping|setToneMappingParams|setTrafficDensity|setTrafficDistance|setTrafficGap|setTrafficSpeed|setTriggerActivation|setTriggerArea|setTriggerStatements|setTriggerText|setTriggerTimeout|setTriggerType|setType|setUnconscious|setUnitAbility|setUnitPos|setUnitPosWeak|setUnitRank|setUnitRecoilCoefficient|setUserActionText|setVariable|setVectorDir|setVectorDirAndUp|setVectorUp|setVehicleAmmo|setVehicleAmmoDef|setVehicleArmor|setVehicleId|setVehicleInit|setVehicleLock|setVehiclePosition|setVehicleTiPars|setVehicleVarName|setVelocity|setVelocityTransformation|setViewDistance|setVisibleIfTreeCollapsed|setWantedRPMRTD|setWaves|setWaypointBehaviour|setWaypointCombatMode|setWaypointCompletionRadius|setWaypointDescription|setWaypointFormation|setWaypointHousePosition|setWaypointLoiterRadius|setWaypointLoiterType|setWaypointName|setWaypointPosition|setWaypointScript|setWaypointSpeed|setWaypointStatements|setWaypointTimeout|setWaypointType|setWaypointVisible|setWeaponReloadingTime|setWind|setWindDir|setWindForce|setWindStr|setWPPos|show3DIcons|showChat|showCinemaBorder|showCommandingMenu|showCompass|showCuratorCompass|showGPS|showHUD|showLegend|showMap|shownArtilleryComputer|shownChat|shownCompass|shownCuratorCompass|showNewEditorObject|shownGPS|shownMap|shownPad|shownRadio|shownUAVFeed|shownWarrant|shownWatch|showPad|showRadio|showSubtitles|showUAVFeed|showWarrant|showWatch|showWaypoint|side|side location|sideChat|sideEnemy|sideFriendly|sideLogic|sideRadio|sideUnknown|simpleTasks|simulationEnabled|simulCloudDensity|simulCloudOcclusion|simulInClouds|simulSetHumidity|simulWeatherSync|sin|size|sizeOf|skill|skillFinal|skill vehicle|skipTime|sleep|sliderPosition|sliderRange|sliderSetPosition|sliderSetRange|sliderSetSpeed|sliderSpeed|slingLoadAssistantShown|soldierMagazines|someAmmo|soundVolume|spawn|speaker|speed|speedMode|sqrt|squadParams|stance|startLoadingScreen|step|stop|stopped|str|sunOrMoon|supportInfo|suppressFor|surfaceIsWater|surfaceNormal|surfaceType|swimInDepth|switch|switch do|switchableUnits|switchAction|switchCamera|switchGesture|switchLight|switchMove|synchronizedObjects|synchronizedTriggers|synchronizedWaypoints|synchronizeObjectsAdd|synchronizeObjectsRemove|synchronizeTrigger|synchronizeWaypoint|synchronizeWaypoint trigger|systemChat|systemOfUnits|tan|targetsAggregate|targetsQuery|taskChildren|taskCompleted|taskDescription|taskDestination|taskHint|taskNull|taskParent|taskResult|taskState|teamMember)\b");
            range.SetStyle(maroonStyle, @"\b(teamMemberNull|teamName|teams|teamSwitch|teamSwitchEnabled|teamType|terminate|terrainIntersect|terrainIntersectASL|text|text location|textLog|textLogFormat|tg|throttleRTD|throw|time|timeMultiplier|titleCut|titleFadeOut|titleObj|titleRsc|titleText|to|toArray|toLower|toString|toUpper|triggerActivated|triggerActivation|triggerArea|triggerAttachedVehicle|triggerAttachObject|triggerAttachVehicle|triggerStatements|triggerText|triggerTimeout|triggerTimeoutCurrent|triggerType|try|turretLocal|turretOwner|turretUnit|tvAdd|tvClear|tvCollapse|tvCount|tvCurSel|tvData|tvDelete|tvExpand|tvPicture|tvSetCurSel|tvSetData|tvSetPicture|tvSetValue|tvSort|tvSortByValue|tvText|tvValue|type|typeName|typeOf|UAVControl|uiNamespace|uiSleep|unassignCurator|unassignItem|unassignTeam|unassignVehicle|underwater|uniform|uniformContainer|uniformItems|uniformMagazines|unitAddons|unitBackpack|unitPos|unitReady|unitRecoilCoefficient|units|unitsBelowHeight|unlinkItem|unlockAchievement|unregisterTask|updateDrawIcon|updateMenuItem|updateObjectTree|useAudioTimeForMoves|vectorAdd|vectorCos|vectorCrossProduct|vectorDiff|vectorDir|vectorDirVisual|vectorDistance|vectorDistanceSqr|vectorDotProduct|vectorFromTo|vectorMagnitude|vectorMagnitudeSqr|vectorMultiply|vectorNormalized|vectorUp|vectorUpVisual|vehicle|vehicleChat|vehicleRadio|vehicles|vehicleVarName|velocity|velocityModelSpace|verifySignature|vest|vestContainer|vestItems|vestMagazines|viewDistance|visibleCompass|visibleGPS|visibleMap|visiblePosition|visiblePositionASL|visibleWatch|waitUntil|waves|waypointAttachedObject|waypointAttachedVehicle|waypointAttachObject|waypointAttachVehicle|waypointBehaviour|waypointCombatMode|waypointCompletionRadius|waypointDescription|waypointFormation|waypointHousePosition|waypointLoiterRadius|waypointLoiterType|waypointName|waypointPosition|waypoints|waypointScript|waypointShow|waypointSpeed|waypointStatements|waypointTimeout|waypointTimeoutCurrent|waypointType|waypointVisible|weaponAccessories|weaponCargo|weaponDirection|weaponLowered|weapons|weaponsItems|weaponsItemsCargo|weaponState|weaponsTurret|weightRTD|west|WFSideText|while|wind|windDir|windStr|with|worldName|worldToModel|worldToModelVisual|worldToScreen|diag_log|_unit|_target|_this|ATLToASL|ATLtoASL|getVariable|getPlayerUID|getPlayerUIDOld|format|formatText|getPos|getDir|headgear|goggles|hint)\b");


            // Set anything else to bold as they're probably variables
            range.SetStyle(boldStyle, @"\w", RegexOptions.Singleline);
        }


        public void PerformSyntaxHighlighting(TextChangedEventArgs e, string lang, bool forceRefresh = false)
        {
            switch (lang)
            {
                case "sqm":
                case "sqs":
                case "sqf":
                    this.SqfHighlight(this.GetActiveEditor(), e, forceRefresh);
                    break;

                case "h":
                case "hpp":
                case "cpp":
                    //EditorHelper.CppHighlight(EditorHelper.GetActiveEditor(), e, forceRefresh);
                    break;

                default:
                    break;
            }
        }


        public AutocompleteMenu CreateArmaSense()
        {
            var editor = MainTabControl.SelectedTab.Controls[0] as FastColoredTextBox;

            //AutocompleteMenu armaSense = new AutocompleteMenu(editor) { SearchPattern = @"[\w\.:=!<>]", AllowTabKey = true };
            
            // Clear out the old Arma Sense
            ArmaSense = null;
            ArmaSense = new AutocompleteMenu(editor)
            {
                SearchPattern = @"[\w\.:=!<>]",
                AllowTabKey = true,
                AppearInterval = 10,
                MinFragmentLength = 1,
                BackColor = Color.White,
                SelectedColor = Color.Khaki,
                MinimumSize = new Size(250, 50),
                ForeColor = Color.Black,
                Font = new Font("Consolas", 10),
                Items = { ImageList = ArmaSenseImageList }
            };

            // TODO - Optimise this -> currently we build a new list of autocomplete items every time we change tab
            //BuildAutocompleteMenu(ArmaSense);

            return ArmaSense;
        }

        public void BuildAutocompleteMenu(AutocompleteMenu armaSense, bool forceUpdate = false)
        {
            // Break out if the number of items hasn't changed -> might cause problems if 1 item is removed, then 1 added
            var itemsCount = (snippets.Length + UserVariablesCurrentFile.Count + ArmaSenseKeywords.Count + 3);
            if(!forceUpdate)
                if (ArmaListItemsCount == itemsCount) return;

            if (armaSense == null)
            {
                armaSense = this.CreateArmaSense();
            }

            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item));

            foreach (var item in UserVariablesCurrentFile)
                items.Add(new AutocompleteItem(item.VarName) { ImageIndex = 1, ToolTipTitle = item.TooltipTitle, ToolTipText = item.TooltipText });
            
            // BACKUP !
            //foreach (var item in userVariables)
            //    items.Add(new AutocompleteItem(item) { ImageIndex = 1 });

            //foreach (var item in methods)
            //    items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });

            items.AddRange(ArmaSenseKeywords);

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            // Set as autocomplete source
            try
            {
                armaSense.Items.SetAutocompleteItems(items);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Logger.Log(ex.Message);
            }

            // Update property
            ArmaListItemsCount = items.Count;
        }

        public void UpdateUserVariables(DoWorkEventArgs loadUserVarsBwDoWork, RunWorkerCompletedEventArgs loadUserVarsBwRunWorkerCompleted)
        {
           
        }

        public string GetVarTypeFromString(string varContents)
        {
            if (
                (varContents.StartsWith("\"") && varContents.EndsWith("\"")) ||
                varContents.StartsWith("str") ||
                varContents.Contains("formatText")
               ) return "String";

            //if (varContents.StartsWith("[") && varContents.EndsWith("]")) return "Array";
            if (varContents.StartsWith("[")) return "Array";

            if (varContents.StartsWith("{")) return "Inline Function";

            if (
                varContents.Contains("compile preprocessFile") ||
                varContents.Contains("compile preprocessFileLineNumbers") ||
                varContents.Contains("execVM") ||
                varContents.Contains("compileFinal preprocessFile") ||
                varContents.Contains("compileFinal preprocessFileLineNumbers")
               ) return "Compiled External Function";
            
            if (
                varContents.Contains("preprocessFile") ||
                varContents.Contains("preprocessFileLineNumbers") ||
                varContents.Contains("loadFile")
               ) return "External Function - UNCOMPILED";
            
            if (varContents == "true" || varContents == "false") return "Boolean";

            if (varContents.StartsWith("group")) return "Group";
            if (varContents.StartsWith("leader")) return "Group Leader";

            if (varContents.StartsWith("side")) return "Side";

            if (varContents.StartsWith("vehicle")) return "Vehicle";
            if (varContents.StartsWith("person")) return "Person";
            if (varContents.StartsWith("rope")) return "Rope";
            if (varContents.StartsWith("unit")) return "Unit";

            double k;
            if (Double.TryParse(varContents, out k) || varContents.StartsWith("count")) return "Number";

            // Catch-all
            return "Object";
        }



        /// <summary>
        /// This item appears when any part of snippet text is typed
        /// </summary>
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Escape(fragmentText);
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }

        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m == null)
                    return fragment;
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            Place enterPlace = Place.Empty;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var r = Parent.Fragment.Clone();
                while (r.Start.iChar > 0)
                {
                    if (r.CharBeforeStart == '}')
                    {
                        enterPlace = r.Start;
                        return CompareResult.Visible;
                    }

                    r.GoLeftThroughFolded();
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                //extend range
                Range r = Parent.Fragment;
                Place end = r.End;
                r.Start = enterPlace;
                r.End = r.End;
                //insert line break
                return Environment.NewLine + r.Text;
            }

            public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
            {
                base.OnSelected(popupMenu, e);
                if (Parent.Fragment.tb.AutoIndent)
                    Parent.Fragment.tb.DoAutoIndent();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }
    }
}
