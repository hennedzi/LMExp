namespace LMExp.Domain.Entitys;

public class Tree
{
    public Node Root { get; set; }
    public Node Current { get; set; }

    public Tree(string value)
    {
        Root = new(value);
        Current = Root;
    }

    public Node? Get(string key)
    {
        string[] keys = key.Split('/');
        if (keys.Length == 0)
            return null;
        if (!Exists(key))
            return null;

        var node = Current.GetNode(keys[0]);

        for (int i = 1; i < keys.Length; i++)
        {
            node = node.GetNode(keys[i]);
        }

        return node;
    }

    public bool Update(string key, string newKey)
    {
        var node = Get(key);
        if (node is null) return false;
        node.Data.Value = newKey;
        return true;
    }

    public void SetCurrent(Node node) => Current = node;

    public void ResetCurrent() => SetCurrent(Root);

    public bool Exists(string key)
    {
        return Root.Exist(key);
    }

    public bool Delete(string key)
    {
        if (string.IsNullOrEmpty(key))
            return false;

        string[] keys = key.Split('/');

        var locationKey = "";

        if (keys.Length == 1)
        {
            locationKey = keys[0];
            var location = Get(locationKey);
            Root.Nodes.Remove(location);
        }
        else
        {
            locationKey += keys[0];
            for (int i = 1; i < keys.Length - 1; i++)
                locationKey += "/" + keys[i];

            if (!Exists(locationKey))
                return false;
            var location = Get(locationKey);
            var toRemove = Get(locationKey + "/" + key);

            location.Nodes.Remove(toRemove);
        }

        return true;
    }

    public bool Insert(string key)
    {
        if (string.IsNullOrEmpty(key))
            return false;

        string[] keys = key.Split('/');

        var locationKey = "";

        if (keys.Length == 1)
        {
            locationKey = keys[0];
            Root.AddNode(locationKey);
        }
        else
        {
            locationKey += keys[0];
            for (int i = 1; i < keys.Length - 1; i++)
                locationKey += "/" + keys[i];

            if (!Exists(locationKey))
                return false;
            var location = Get(locationKey);

            location?.AddNode(key);
        }

        return true;
    }
}