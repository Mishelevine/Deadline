using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using System.Linq;

namespace COMMANDS
{
    public class CMD_DatabaseExtention_Characters : CMD_DatabaseExtension
    {
        private static string[] PARAM_ENABLE => new string[] { "-e", "-enable" };
        private static string[] PARAM_IMMEDIATE => new string[] { "-i", "-immediate" };
        private static string PARAM_XPOS => "-x";
        private static string[] PARAM_SPEED => new string[] { "-spd", "-speed" };
        private static string[] PARAM_SMOOTH => new string[] { "-sm", "-smooth" };

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("createcharacter", new Action<string[]>(CreateCharacter));
            database.AddCommand("show", new Func<string[], IEnumerator>(ShowAll));
            database.AddCommand("hide", new Func<string[], IEnumerator>(HideAll));
            database.AddCommand("movecharacter", new Func<string[], IEnumerator>(MoveCharacter));
            database.AddCommand("sort", new Action<string[]>(Sort));

            CommandDatabase baseCommands = CommandManager.instance.CreateSubDatabase(CommandManager.DATABASE_CHARACTERS_BASE);
            baseCommands.AddCommand("move", new Func<string[], IEnumerator>(MoveCharacter));
            baseCommands.AddCommand("show", new Func<string[], IEnumerator>(ShowAll));
            baseCommands.AddCommand("hide", new Func<string[], IEnumerator>(HideAll));
            baseCommands.AddCommand("setpriority", new Action<string[]>(SetPriority));
            baseCommands.AddCommand("setposition", new Action<string[]>(SetPosition));
            baseCommands.AddCommand("setColor", new Func<string[], IEnumerator>(SetColor));
            baseCommands.AddCommand("highlight", new Func<string[], IEnumerator>(Highlight));
            baseCommands.AddCommand("unhighlight", new Func<string[], IEnumerator>(Unhighlight));

            CommandDatabase spriteCommands = CommandManager.instance.CreateSubDatabase(CommandManager.DATABASE_CHARACTERS_SPRITE);
            baseCommands.AddCommand("setsprite", new Func<string[], IEnumerator>(SetSprite));
        }

        public static void CreateCharacter(string[] data)
        {
            bool enable = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_ENABLE, out enable, defaultValue: false);

            if (data.Contains("-e") || data.Contains("-enable"))
                data = data.Where(x => !(x.StartsWith("-") || x.StartsWith("true") || x.StartsWith("false"))).ToArray();

            string characterName = string.Join(" ", data);

            CharacterManager.instance.CreateCharacter(characterName, revealAfterCreation: enable);
        }

        public static void Sort(string[] data)
        {
            CharacterManager.instance.SortCharacters(data);
        }

        public static IEnumerator MoveCharacter(string[] data)
        {
            float x = 0;
            float speed = 1;
            bool smooth = false;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_XPOS, out x);

            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1);

            parameters.TryGetValue(PARAM_SMOOTH, out smooth, defaultValue: false);

            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (data.Contains("-i") || data.Contains("-immediate") || data.Contains("-x") || data.Contains("-spd") || data.Contains("-speed") || data.Contains("-sm") || data.Contains("-smooth"))
                data = data.Where(x => !(x.StartsWith("-") || x.StartsWith("true") || x.StartsWith("false"))).Where(x => !float.TryParse(x, out _)).ToArray();

            string characterName = string.Join(" ", data);

            Character character = CharacterManager.instance.GetCharacter(characterName, createIfDoesNotExist: false);

            if (character == null)
                yield break;

            Vector2 position = new(x, 0);

            if (immediate)
                character.SetPosition(position);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.SetPosition(position); });
                yield return character.MoveToPosition(position, speed, smooth);
            }
        }

        public static IEnumerator ShowAll(string[] data)
        {
            List<Character> characters = new List<Character>();

            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (data.Contains("-i") || data.Contains("-immediate"))
                data = data.Where(x => !(x.StartsWith("-") || x.StartsWith("true") || x.StartsWith("false"))).ToArray();

            string characterName = string.Join(" ", data);

            Character character = CharacterManager.instance.GetCharacter(characterName, createIfDoesNotExist: false);

            if (character != null)
                characters.Add(character);

            if (characters.Count == 0)
                yield break;

            if (immediate)
                character.isVisible = true;
            else
                character.Show();

            if (!immediate)
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
                {
                    foreach (Character character in characters)
                        character.isVisible = true;
                });

                while (characters.Any(c => c.isRevealing))
                    yield return null;
            }
        }

        public static IEnumerator HideAll(string[] data)
        {
            List<Character> characters = new List<Character>();

            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (data.Contains("-i") || data.Contains("-immediate"))
                data = data.Where(x => !(x.StartsWith("-") || x.StartsWith("true") || x.StartsWith("false"))).ToArray();

            string characterName = string.Join(" ", data);

            Character character = CharacterManager.instance.GetCharacter(characterName, createIfDoesNotExist: false);

            if (character != null)
                characters.Add(character);

            if (characters.Count == 0)
                yield break;

            if (immediate)
                character.isVisible = false;
            else
                character.Hide();

            if (!immediate)
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
                {
                    foreach (Character character in characters)
                        character.isVisible = false;
                });

                while (characters.Any(c => c.isHiding))
                    yield return null;
            }
        }

        public static void SetPosition(string[] data)
        {
            Character character = CharacterManager.instance.GetCharacter(data[0], createIfDoesNotExist: false);

            float x = 0;

            if (character == null || data.Length < 2)
                return;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_XPOS, out x, defaultValue: 0);

            character.SetPosition(new Vector2(x, 0));
        }

        public static void SetPriority(string[] data)
        {
            Character character = CharacterManager.instance.GetCharacter(data[0], createIfDoesNotExist: false);

            int priority = 0;

            if (character == null || data.Length < 2)
                return;

            if (!int.TryParse(data[1], out priority))
                priority = 0;

            character.SetPriority(priority);
        }

        public static IEnumerator SetColor(string[] data)
        {
            Character character = CharacterManager.instance.GetCharacter(data[0], createIfDoesNotExist: false);
            string colorName;
            float speed;
            bool immediate;

            if(character == null || data.Length < 2)
                yield break;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(new string[] { "-c", "-color"}, out colorName);

            bool specifiedSpeed = parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1);

            if(!specifiedSpeed)
                parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);
            else
                immediate = false;

            Color color = Color.white;
            color = color.GetColorFromName(colorName);

            if(immediate)
                character.SetColor(color);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.SetColor(color); });
                character.TransitionColor(color, speed);
            }

            yield break;
        }

        public static IEnumerator Highlight(string[] data)
        {
            Character character = CharacterManager.instance.GetCharacter(data[0], createIfDoesNotExist: false);

            if (character == null)
                yield break;

            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if(immediate)
                character.Highlight(immediate: true);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.Highlight(immediate: true); });
                yield return character.Highlight();
            }
        }

        public static IEnumerator Unhighlight(string[] data)
        {
            Character character = CharacterManager.instance.GetCharacter(data[0], createIfDoesNotExist: false);

            if (character == null)
                yield break;

            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            if (immediate)
                character.UnHighlight(immediate: true);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.UnHighlight(immediate: true); });
                yield return character.UnHighlight();
            }
        }

        public static IEnumerator SetSprite(string[] data)
        {
            Character_Sprite character = CharacterManager.instance.GetCharacter(data[0], createIfDoesNotExist: false) as Character_Sprite;
            string spriteName;
            bool immediate = false;
            float speed;

            if (character == null || data.Length < 2)
                yield break;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(new string[] { "-s", "-sprite" }, out spriteName);

            bool specifiedSpeed = parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1);

            if (!specifiedSpeed)
                parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);
            else
                immediate = false;

            Sprite sprite = character.GetSprite(spriteName);

            if (sprite == null)
                yield break;

            if (immediate)
            {
                character.SetSprite(sprite);
            }
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.SetSprite(sprite); });
                yield return character.TransitionSprite(sprite, 0, speed);
            }
        }
    }
}