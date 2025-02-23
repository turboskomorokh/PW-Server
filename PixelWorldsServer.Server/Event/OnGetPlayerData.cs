using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.DataAccess;
using PixelWorldsServer.DataAccess.Models;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.GET_PLAYER_DATA_KEY)]
public class OnGetPlayerData : IEvent
{
  // TODO: fix duplicates
  private static readonly string[] m_TutorialWorldLists = new string[11]
    {
        "TUTORIAL1",
        "TUTORIAL2",
        "TUTORIAL3",
        "TUTORIAL4",
        "TUTORIAL5",
        "TUTORIAL6",
        "TUTORIAL7",
        "TUTORIAL8",
        "TUTORIAL9",
        "TUTORIAL10",
        "TUTORIAL11"
    };
  public async Task Invoke(EventContext context, BsonDocument document)
  {
    GetPlayerDataRequest request = BsonSerializer.Deserialize<GetPlayerDataRequest>(document);
    if (context.Database == null)
      throw new Exception("Database is null");
    Console.WriteLine("[EH] Triggered OnGetPlayerData");
    PlayerModel? playerModel = null;
    if (request.Token.Length == 24)
    {
      playerModel = await context.Database.GetPlayerByIdAsync(request.Token).ConfigureAwait(false);
    }

    if (playerModel is null)
    {
      playerModel = await context.Database.CreatePlayerAsync(context.Player.IP, context.Player.OS).ConfigureAwait(false);
      context.Player.SendPacket(new LoginTokenUpdateResponse()
      {
        ID = NetStrings.LOGIN_TOKEN_UPDATE_KEY,
        Token = playerModel.Id
      });
    }

    context.Player.LoadCopy(playerModel);

    var playerData = new PlayerDataResponse()
    {
      Gems = context.Player.Gems,
      Slots = context.Player.Slots,
      Spots = context.Player.Spots,
      Gender = context.Player.Gender,
      Username = context.Player.Name,
      XPAmount = context.Player.XP,
      ByteCoins = context.Player.ByteCoins,
      Inventory = context.Player.PackInventory(),
      Belt1Item = context.Player.Belt1Item,
      IsStarter = context.Player.IsStarter,
      VIPEndTime = context.Player.VIPEndTime.Ticks,
      Statistics = context.Player.Statistics,
      AccountAge = context.Player.DateCreated.Ticks,
      CountryCode = context.Player.CountryCode,
      AdminStatus = (int)context.Player.AdminStatus,
      FamiliarName = context.Player.FamiliarName,
      ShowLocation = context.Player.ShowLocation,
      PlayerCostume = context.Player.Costume,
      InventoryData = context.Player.InventoryItemData.ToBsonDocument(),
      TutorialState = (int)context.Player.TutorialState,
      QuestCurrentID = context.Player.QuestCurrentID,
      QuestListCount = context.Player.QuestListCount,
      SkinColorIndex = context.Player.Skin,
      PassiveEffects = context.Player.PassiveEffects,
      CameraZoomLevel = (int)context.Player.CameraZoomLevel,
      CameraZoomValue = context.Player.CameraZoomValue,
      ShowOnlineStatus = context.Player.ShowOnlineStatus,
      IsFamiliarMaxLvl = context.Player.IsFamiliarMaxLvl,
      LastVIPClaimTime = context.Player.LastVIPClaimTime.Ticks,
      ExperienceAmount = context.Player.ExperienceAmount, // what the fuck is the difference with XP
      WelcomeGiftIndex = context.Player.WelcomeGiftIndex,
      QuestCurrentPhase = context.Player.QuestCurrentPhase,
      NameChangeCounter = context.Player.NameChangeCounter,
      InstructionStates = context.Player.InstructionStates,
      FamiliarBlockType = context.Player.FamiliarBlockType,
      LastNormalClaimTime = context.Player.LastNormalClaimTime.Ticks,
      PlayerCostumeEndTime = context.Player.CostumeEndTime.Ticks,
      FaceExpressionListID = context.Player.FaceExpressionListId,
      BoughtExpressionsList = context.Player.BoughtExpressionsList,
      NextVIPDailyBonusClaim = context.Player.NextVIPDailyBonusClaim.Ticks,
      NextDailyBonusGiveAway = context.Player.NextDailyBonusGiveAway.Ticks,
      LastFreePrizeClaimTime = context.Player.LastFreePrizeClaimTime.Ticks,
      NextDailyAdsRefreshTime = context.Player.NextDailyAdsRefreshTime.Ticks,
      DailyQuestNextAvailList = context.Player.DailyQuestNextAvailList,
      NextWelcomeGiftClaimTime = context.Player.NextWelcomeGiftClaimTime.Ticks,
      InstructionStatesAmounts = context.Player.InstructionStatesAmounts,
      AchievementCurrentValues = context.Player.AchievementCurrentValues,
      AchievementRewardsClaimed = context.Player.AchievementRewardsClaimed,
      DefaultFaceAnimationIndex = context.Player.DefaultFaceAnimationIndex,
      NextNormalDailyBonusClaim = context.Player.NextNormalDailyBonusClaim.Ticks,
      AlreadyBoughtOneTimeItems = context.Player.AlreadyBoughtOneTimeItems,
      PreviousThreeDailyQuestIds = context.Player.PreviousThreeDailyQuestIds,
      TutorialQuestCompleteState = context.Player.TutorialQuestCompleteState,
      AchievementsCompletedStates = context.Player.AchievementsCompletedStates,
      SkinColorIndexBeforeOverride = context.Player.SkinColorIndexBeforeOverride,
      NextDailyPvpRewardsRefreshTime = context.Player.NextDailyPvpRewardsRefreshTime.Ticks,
      CardGameFaceExpressionsEnabled = context.Player.CardGameFaceExpressionsEnabled,
      CardGameBodyExpressionsEnabled = context.Player.CardGameBodyExpressionsEnabled,
      HasClaimedAdditionalAdBasedDailyBonus = context.Player.HasClaimedAdditionalAdBasedDailyBonus,

      Tutorial1CurrentStep = 0,
      Tutorial1TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial2CurrentStep = 0,
      Tutorial2TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial3CurrentStep = 0,
      Tutorial3TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial4CurrentStep = 0,
      Tutorial4TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial5CurrentStep = 0,
      Tutorial5TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial5InventorySize = 0,
      Tutorial6CurrentStep = 0,
      Tutorial6TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial7CurrentStep = 0,
      Tutorial7TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial8CurrentStep = 0,
      Tutorial8TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial8QuestVisitedWorldsList = Array.Empty<int>(),
      Tutorial9CurrentStep = 0,
      Tutorial9TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial9QuestVisitedWorldsList = Array.Empty<int>(),
      Tutorial10CurrentStep = 0,
      Tutorial10TrackQuestStepProgress = Array.Empty<int>(),
      Tutorial11CurrentStep = 0,
      Tutorial11TrackQuestStepProgress = Array.Empty<int>(),
      TutorialIDList = m_TutorialWorldLists,
      LimitedOffers = new BsonDocument()
            {
                { "Count", 0 }
            },
      LimitedOffersUsed = new BsonDocument()
            {
                { "Count", 0 }
            },
      FTUESoldItemIDs = new BsonDocument()
            {
                { "Count", 0 }
            },
      GenericVersioning = new int[] { 62 },
    };

    var response = new GetPlayerDataResponse()
    {
      ID = NetStrings.GET_PLAYER_DATA_KEY,
      WOTW = "NUTS",
      Email = "",
      BanState = 0,
      PlayerId = context.Player.Id.ToUpper(),
      WorldNames = Array.Empty<string>(),
      PlayerData = playerData.ToBson(),
      NewsVersion = 62,
      WOTWVersion = 225,
      RealUsername = context.Player.Name,
      EmailVerified = false,
      PlayerUsername = context.Player.Name,
    };

    context.Player.SendPacket(response);
  }
}