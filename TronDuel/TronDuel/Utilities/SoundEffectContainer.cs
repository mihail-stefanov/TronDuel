namespace TronDuel.Utilities
{
    using System.Media;

    public class SoundEffectContainer
    {
        private SoundPlayer startPlayer = new SoundPlayer();
        private SoundPlayer powerupSoundPlayer = new SoundPlayer();
        private SoundPlayer shieldSoundPlayer = new SoundPlayer();
        private SoundPlayer shotSoundPlayer = new SoundPlayer();
        private SoundPlayer enemyShotSoundPlayer = new SoundPlayer();
        private SoundPlayer ammoLoadPlayer = new SoundPlayer();
        private SoundPlayer hitPlayer = new SoundPlayer();
        private SoundPlayer dullHitPlayer = new SoundPlayer();
        private SoundPlayer explosionPlayer = new SoundPlayer();
        private SoundPlayer tronBonusPlayer = new SoundPlayer();
        public SoundEffectContainer()
        {
            LoadSounds();
        }

        private void LoadSounds()
        {
            // Loading sounds
            this.startPlayer.SoundLocation = "start.wav";
            this.startPlayer.LoadAsync();
            this.powerupSoundPlayer.SoundLocation = "powerUp.wav";
            this.powerupSoundPlayer.LoadAsync();
            this.shieldSoundPlayer.SoundLocation = "shield.wav";
            this.shieldSoundPlayer.LoadAsync();
            this.shotSoundPlayer.SoundLocation = "shot.wav";
            this.shotSoundPlayer.LoadAsync();
            this.enemyShotSoundPlayer.SoundLocation = "enemyShot.wav";
            this.enemyShotSoundPlayer.LoadAsync();
            this.ammoLoadPlayer.SoundLocation = "ammoLoad.wav";
            this.ammoLoadPlayer.LoadAsync();
            this.hitPlayer.SoundLocation = "hit.wav";
            this.hitPlayer.LoadAsync();
            this.dullHitPlayer.SoundLocation = "dullHit.wav";
            this.dullHitPlayer.LoadAsync();
            this.explosionPlayer.SoundLocation = "explosion.wav";
            this.explosionPlayer.LoadAsync();
            this.tronBonusPlayer.SoundLocation = "tronBonus.wav";
            this.tronBonusPlayer.LoadAsync();
        }

        public void PlayStart()
        {
            this.startPlayer.Play();
        }

        public void PlayPowerUp()
        {
            this.powerupSoundPlayer.Play();
        }

        public void PlayShieldPowerUp()
        {
            this.shieldSoundPlayer.Play();
        }

        public void PlayShot()
        {
            this.shotSoundPlayer.Play();
        }

        public void PlayEnemyShot()
        {
            this.enemyShotSoundPlayer.Play();
        }

        public void PlayAmmoLoad()
        {
            this.ammoLoadPlayer.Play();
        }

        public void PlayHit()
        {
            this.hitPlayer.Play();
        }

        public void PlayDullHit()
        {
            this.dullHitPlayer.Play();
        }

        public void PlayExplosion()
        {
            this.explosionPlayer.Play();
        }

        public void PlayTronBonus()
        {
            this.tronBonusPlayer.Play();
        }
    }
}
