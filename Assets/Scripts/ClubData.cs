using UnityEngine;
using SQLite;

public class ClubData
{
    public int level;
    public int id;
    public int maxAudience;
    public float experience;
    public float reputation;



    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Club : MonoBehaviour
{

}