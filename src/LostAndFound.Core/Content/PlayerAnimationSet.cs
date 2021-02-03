using System.Collections.Generic;
using Asepreadr.Aseprite;
using Asepreadr.Graphics;
using Asepreadr.Loaders;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Graphics;

namespace LostAndFound.Core.Content
{
    public class PlayerAnimationSet : IAnimationSet
    {
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;

        public PlayerAnimationSet(IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _spriteMapLoader = spriteMapLoader;
        }

        public void AddTo(AnimatorComponent animatorComponent)
        {
            var playerAnimationMap = _spriteMapLoader.GetContent("Assets/Images/Player/PlayerAnimations.json");

            animatorComponent.AddAnimation("Walk_Right", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_5"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_6")
            })
            {
                FrameDuration = 0.2f
            });

            animatorComponent.AddAnimation("Walk_Left", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_5"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_6")
            })
            {
                FrameDuration = 0.2f
            });

            animatorComponent.AddAnimation("Walk_Up", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_5"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_6")
            })
            {
                FrameDuration = 0.2f
            });

            animatorComponent.AddAnimation("Walk_Down", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_5"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_6")
            })
            {
                FrameDuration = 0.2f
            });

            animatorComponent.AddAnimation("Idle", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Idle_1"),
                playerAnimationMap.CreateSpriteFromRegion("Idle_2")
            })
            {
                FrameDuration = 0.6f
            });
        }
    }
}