using Asteroids.Game.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.UI
{
 public class ShipStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coordinatesLabel;
        [SerializeField] private TextMeshProUGUI rotationLabel;
        [SerializeField] private TextMeshProUGUI speedLabel;
        [SerializeField] private TextMeshProUGUI laserChargesLabel;
        [SerializeField] private TextMeshProUGUI laserCooldownLabel;

        [Inject]
        private void Init(SignalService signalService)
        {
            signalService.Subscribe<UpdatePlayerPositionSignal>(OnPositionUpdated);
            signalService.Subscribe<UpdatePlayerRotationSignal>(OnRotationUpdated);
            signalService.Subscribe<UpdatePlayerSpeedSignal>(OnSpeedUpdated);
            signalService.Subscribe<UpdatePlayerLaserChargesSignal>(OnLaserChargesUpdated);
            signalService.Subscribe<UpdatePlayerLaserCooldownSignal>(OnLaserCooldownUpdated);
        }

        private void OnPositionUpdated(UpdatePlayerPositionSignal signal)
        {
            coordinatesLabel.text = $"Coordinates: ({signal.Value.x:F2}, {signal.Value.y:F2})";
        }

        private void OnRotationUpdated(UpdatePlayerRotationSignal signal)
        {
            rotationLabel.text = $"Rotation: {signal.Value:F2}°";
        }

        private void OnSpeedUpdated(UpdatePlayerSpeedSignal signal)
        {
            speedLabel.text = $"Speed: {signal.Value:F2}";
        }

        private void OnLaserChargesUpdated(UpdatePlayerLaserChargesSignal signal)
        {
            laserChargesLabel.text = $"Laser Charges: {signal.Value}";
        }

        private void OnLaserCooldownUpdated(UpdatePlayerLaserCooldownSignal signal)
        {
            laserCooldownLabel.text = $"Laser Cooldown: {signal.Value:F1}s";
        }
    }
}