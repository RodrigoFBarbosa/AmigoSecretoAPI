using AmigoSecretoAPI.Models;

namespace AmigoSecretoAPI.Services;

public class APIService
{
    private readonly List<Group> _groups = new();


    public Group CreateGroup(string name)
    {
        var group = new Group { Name = name };
        _groups.Add(group);
        return group;
    }

    public Group? GetGroup(Guid groupId) => _groups.FirstOrDefault(g => g.Id == groupId);

    public void AddParticipant(Guid groupId, Participant participant)
    {
        var group = GetGroup(groupId);
        if (group == null) throw new ArgumentException("Group not found.");
        group.Participants.Add(participant);
    }

    public void GenerateMatches(Guid groupId)
    {
        var group = GetGroup(groupId); ;
        if (group == null) throw new ArgumentException("Group not found.");
        if (group.Participants.Count < 2)
            throw new InvalidOperationException("At least 2 participants are required to generate matches.");

        var shuffled = group.Participants.OrderBy(_ => Guid.NewGuid()).ToList();
        group.Matches = shuffled.Select((p, i) => (p, shuffled[(i + 1) % shuffled.Count])).ToList();
    }

    public Participant? GetMysteryFriend(Guid groupId, Guid participantId)
    {
        var group = GetGroup(groupId);
        return group?.Matches.FirstOrDefault(m => m.Item1.Id == participantId).Item2;
    }
}
