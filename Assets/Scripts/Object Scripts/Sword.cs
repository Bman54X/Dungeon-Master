using UnityEngine;

public class Sword : MonoBehaviour {
	bool _swinging, _hitOnce, _strongHit;
    string _swinger;

	// Use this for initialization
	void Start () {
		swinging = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_swinging) {
			_hitOnce = false;
		}
	}

    public string swinger {
        get { return _swinger; }
        set { _swinger = value; }
    }

    public bool swinging {
		get { return _swinging; }
		set { _swinging = value;}
	}

	public bool hitOnce {
		get { return _hitOnce; }
		set { _hitOnce = value;}
	}

    public bool strongHit {
        get { return _strongHit; }
        set { _strongHit = value; }
    }
}