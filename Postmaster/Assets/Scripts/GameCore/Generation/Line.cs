using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour, IPauseHandler
{
    [SerializeField] private ClientGenerator _generator;
	[SerializeField] private OrderTable _orderTable;

	[Header("SPAWN DELAY")]
	[Space(5)]
	[SerializeField] [Range(1, 2)] private float minSpawnTime;
	[SerializeField] [Range(2.1f, 5)] private float maxSpawnTime;

	[Header("SPAWN POSITION")]
	[Space(5)]
	[SerializeField] private List<Place> _places = new List<Place>();	
	[SerializeField] private Transform _spawnPoint;
	private Dictionary<Place, Client> _clients;
	private List<ClientData> _clientsData = new List<ClientData>();

	private bool _isPaused;

	private void Start()
    {
		_orderTable.OrderComplited.AddListener(UpdateLine);

		_clients = new Dictionary<Place, Client>();
		_clients.Clear();

        foreach (Place place in _places)
        {
			_clients.Add(place, null);
		}

		StartCoroutine(FillLine());

		_isPaused = PauseManager.pauseManager.IsPaused;
		PauseManager.pauseManager.Register(this);
	}	
	
	private IEnumerator FillLine()
	{
		List<Place> freePlaces = new List<Place>();

		foreach (Place place in _places)
		{
			if (place.IsFree == true)
			{
				freePlaces.Add(place);
			}
			else if (freePlaces[0] != null)
            {
				Client script = _clients[place];

				yield return new WaitWhile(() => _isPaused == true);

				script.GoTo(freePlaces[0].gameObject.transform);
				_clients[freePlaces[0]] = _clients[place];				
				_clients[place] = null;
			
				freePlaces[0].IsFree = false;
				place.IsFree = true;

				freePlaces.Add(place);
				freePlaces.RemoveAt(0);						
			}
			float wait = Random.Range(0.5f, 1.0f);
			yield return new WaitForSeconds(wait);
		}		

		foreach (Place place in freePlaces)
		{
			float rand = Random.Range(minSpawnTime, maxSpawnTime);

			yield return new WaitForSeconds(rand);

			ClientData data = _generator.GetClient();

			yield return new WaitWhile(() => _isPaused == true);

			GameObject prefab = SpawnClient(data.Prefab);
			Client script = prefab.GetComponent<Client>();			
			script.Init(data);
			script.GoTo(place.gameObject.transform);

			_clients[place] = script;
			place.IsFree = false;

			_clientsData.Add(data);
		}
	}
	
	private GameObject SpawnClient(GameObject prefab)
	{
		GameObject spawnedClient = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
		return spawnedClient;
	}

	private void UpdateLine(float cash)
    {
		_generator.ReturnClient(_clientsData[0]);
		_clientsData.RemoveAt(0);
		_clients[_places[0]] = null;
		_places[0].IsFree = true;
		StopCoroutine(FillLine());
		StartCoroutine(FillLine());
	}

	public void SetPause(bool isPaused)
	{
		_isPaused = isPaused;
	}
}
