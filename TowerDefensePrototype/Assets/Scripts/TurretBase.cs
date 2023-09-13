using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBase : MonoBehaviour {
    private Turret installedTurret;

    public Turret GetInstalledTurret() {
        return installedTurret;
    }

    public void SetInstalledTurret(Turret _installedTurret) {
        installedTurret = _installedTurret;
    }
}
