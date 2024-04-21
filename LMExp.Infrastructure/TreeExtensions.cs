using LMExp.Domain;

namespace LMExp.Infrastructure;

public static class TreeExtensions
{
    public static Node? Get(this Tree tree, string key)
    {
        string[] keys = key.Split('/');
        if (keys.Length == 0)
            return null;
        if (!tree.Exists(key))
            return null;

        var node = tree.Current.GetNode(keys[0]);

        for (int i = 1; i < keys.Length; i++)
        {
            node = node.GetNode(keys[i]);
        }

        return node;
    }

    public static bool Update(this Tree tree, string key, string newKey)
    {
        var node = tree.Get(key);
        if (node is null) return false;
        node.Data.Value = newKey;
        return true;
    }

    public static void SetCurrent(this Tree tree, Node node) => tree.Current = node;

    public static void ResetCurrent(this Tree tree) => SetCurrent(tree, tree.Root);

    public static bool Exists(this Tree tree, string key)
    {
        return tree.Root.Exist(key);
    }

    public static bool Delete(this Tree tree, string key)
    {
        if (string.IsNullOrEmpty(key))
            return false;

        string[] keys = key.Split('/');

        var locationKey = "";

        if (keys.Length == 1)
        {
            locationKey = keys[0];
            var location = tree.Get(locationKey);
            tree.Root.Nodes.Remove(location);
        }
        else
        {
            locationKey += keys[0];
            for (int i = 1; i < keys.Length - 1; i++)
                locationKey += "/" + keys[i];

            if (!tree.Exists(locationKey))
                return false;
            var location = tree.Get(locationKey);
            var toRemove = tree.Get(locationKey + "/" + key);

            location.Nodes.Remove(toRemove);
        }
        return true;
    }

    public static bool Insert(this Tree tree, string key)
    {
        if (string.IsNullOrEmpty(key))
            return false;

        string[] keys = key.Split('/');

        var locationKey = "";

        if (keys.Length == 1)
        {
            locationKey = keys[0];
            tree.Root.AddNode(locationKey);
        }
        else
        {
            locationKey += keys[0];
            for (int i = 1; i < keys.Length - 1; i++)
                locationKey += "/" + keys[i];

            if (!tree.Exists(locationKey))
                return false;
            var location = tree.Get(locationKey);

            location?.AddNode(key);
        }
        return true;
    }
}