using COMMANDS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace CHARACTERS
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager instance { get; private set; }

        private Dictionary<string, Character> characters = new Dictionary<string, Character>();

        private CharacterConfigSO config => DialogueSystem.instance.config.characterConfigurationAsset;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                DestroyImmediate(gameObject);
        }

        public CharacterConfigData GetCharacterConfig(string characterName)
        {
            return config.GetConfig(characterName);
        }

        public Character GetCharacter(string characterName, bool createIfDoesNotExist = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
                return characters[characterName.ToLower()];
            else if (createIfDoesNotExist)
                return CreateCharacter(characterName);

            return null;
        }

        public Character CreateCharacter(string characterName)
        {
            if (characters.ContainsKey(characterName.ToLower()))
            {
                Debug.LogWarning($"A character {characterName} already exists");
                return null;
            }

            CHARACTER_INFO info = GetCharacterInfo(characterName);

            Character character = CreateCharacterFromInfo(info);

            characters.Add(characterName.ToLower(), character);

            return character;
        }

        private CHARACTER_INFO GetCharacterInfo(string characterName)
        {
            CHARACTER_INFO result = new CHARACTER_INFO();

            result.name = characterName;

            result.config = config.GetConfig(characterName);

            return result;
        }

        private Character CreateCharacterFromInfo(CHARACTER_INFO info)
        {
            CharacterConfigData config = info.config;

            if (config.characterType == Character.CharacterType.Text)
                return new Character_Text(info.name, config);

            if (config.characterType == Character.CharacterType.Sprite || config.characterType == Character.CharacterType.SpriteSheet)
                return new Character_Sprite(info.name, config);

            return null;
        }

        private class CHARACTER_INFO
        {
            public string name = "";

            public CharacterConfigData config = null;
        }
    }
}