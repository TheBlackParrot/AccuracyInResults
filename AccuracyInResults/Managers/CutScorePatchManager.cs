using System;
using JetBrains.Annotations;
using Zenject;

namespace AccuracyInResults.Managers;

[UsedImplicitly]
internal class CutScorePatchManager : IInitializable, IDisposable
{
    private static ScoreController _scoreController = null!;

    public static int MaxScoreWithoutMisses;
    public static int ScoreWithoutMisses;

    public CutScorePatchManager(ScoreController scoreController)
    {
        _scoreController = scoreController;
    }

    public void Initialize()
    {
        MaxScoreWithoutMisses = 0;
        ScoreWithoutMisses = 0;
        
        _scoreController.scoringForNoteFinishedEvent += ScoreControllerOnScoringForNoteFinishedEvent;
    }
    
    public void Dispose()
    {
        _scoreController.scoringForNoteFinishedEvent -= ScoreControllerOnScoringForNoteFinishedEvent;
    }

    private static void ScoreControllerOnScoringForNoteFinishedEvent(ScoringElement scoringElement)
    {
        if (scoringElement is not GoodCutScoringElement)
        {
            return;
        }
        
        MaxScoreWithoutMisses += scoringElement.maxPossibleCutScore * scoringElement.maxMultiplier;
        //Plugin.DebugMessage($"Maximum FC Score: {_maxScoreWithoutMisses} (+ {scoringElement.maxPossibleCutScore})");

        ScoreWithoutMisses += scoringElement.cutScore * scoringElement.maxMultiplier;
        //Plugin.DebugMessage($"FC Score: {_scoreWithoutMisses} (+ {scoringElement.cutScore})");

        //float acc = ScoreWithoutMisses / (float)MaxScoreWithoutMisses;
        //Plugin.DebugMessage($"FC Acc: {acc * 100}%");
    }
}