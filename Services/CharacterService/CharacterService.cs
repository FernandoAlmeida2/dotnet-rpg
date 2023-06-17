namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };
        public List<Character> getAllCharacters()
        {
            return characters;
        }

        public Character getCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if (character is null)
            {
                throw new Exception("Character not found");
            }
            return character;
        }

        public List<Character> saveCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }
    }
}