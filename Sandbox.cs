namespace Sandbox
{
	private class Sandbox
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Gun
		// Token: 0x06000B9B RID: 2971 RVA: 0x000DF904 File Offset: 0x000DDB04
		public Bullet spawnBullet(bulletStatus bulletType, InvItem myWeapon, int bulletNetID, bool specialAbility, string myStatusEffect)
		{
			int num;
			if (bulletType == bulletStatus.Shotgun)
				num = 3;
			else
				num = 1;
			
			Bullet bullet = null;
			PlayfieldObject playfieldObject = null;
			bool autoAim = false;
			bool autoAimApplied = false;
			bool shotgunFlag = false;
			bool isSilenced = false;
			bool isRubberBullet = false;
			float num2 = 999f;
			float recoilEffect = 999f;
			int agentAccuracy = this.agent.accuracyStatMod; //num4

			if (this.agent.statusEffects.hasTrait("Withdrawal"))
			{
				agentAccuracy -= 2;

				if (agentAccuracy < 0)
					agentAccuracy = 0;
			}

			if (bulletType == bulletStatus.ResearchGun)
				agentAccuracy = 4;
			
			if (myWeapon != null)
			{
				if (myWeapon.contents.Contains("Silencer"))
					isSilenced = true;
			
				if (myWeapon.contents.Contains("RubberBulletsMod"))
					isRubberBullet = true;
			}

			List<Bullet> list = new List<Bullet>();
			int bulletsHandled = 0;
			
			while (bulletsHandled < num)
			{
				if (this.bulletNetIDList.Count > 0)
				{
					try
					{
						bullet = this.gc.spawnerMain.SpawnBullet(this.tr.position, bulletType, this.agent, this.bulletNetIDList[bulletsHandled]);
					}
					catch { }
				}

				bullet = this.gc.spawnerMain.SpawnBullet(this.tr.position, bulletType, this.agent, bulletNetID);
				bullet.gun = myWeapon;
				bullet.statusEffect = myStatusEffect;
				int moreAccuracy = 0; // num5

				if (myWeapon != null)
				{
					if (myWeapon.contents.Contains("AccuracyMod"))
					{
						moreAccuracy++;
						bullet.moreAccuracy++;

						if (!myWeapon.rapidFire)
						{
							this.agent.weaponCooldown -= 0.1f;
							this.agent.weaponCooldown = Mathf.Clamp(this.agent.weaponCooldown, 0.09f, 100f);
						}
					}

					if (this.agent.statusEffects.hasTrait("Accurate"))
					{
						moreAccuracy += 2;
						bullet.moreAccuracy += 2;

						if (!myWeapon.rapidFire)
						{
							this.agent.weaponCooldown -= 0.2f;
							this.agent.weaponCooldown = Mathf.Clamp(this.agent.weaponCooldown, 0.09f, 100f);
						}
					}

					if (myWeapon.contents.Contains("RubberBulletsMod") || isRubberBullet)
						bullet.rubber = true;
				}

				if (this.agent.isPlayer > 0 && !this.agent.outOfControl && this.agent.localPlayer)
				{
					this.agent.agentSpriteContainer.rotation = Quaternion.identity;
					this.agent.agentSpriteTransform.rotation = Quaternion.identity;

					if (this.agent.controllerType == "Keyboard" && !this.gc.sessionDataBig.trackpadMode)
						bullet.movement.RotateToMouseTr(this.agent.agentCamera.actualCamera);
					else if (this.agent.target.AttackTowardTarget())
						bullet.tr.rotation = Quaternion.Euler(0f, 0f, this.agent.target.transform.eulerAngles.z);
					else
						bullet.tr.rotation = Quaternion.Euler(0f, 0f, this.FindWeaponAngleGamepad() - 90f);

					if (!autoAimApplied && this.gc.sessionDataBig.autoAim != "Off" && bulletType != bulletStatus.ResearchGun)
					{
						int myChance = (agentAccuracy + 1 + moreAccuracy) * 25;

						if (this.gc.percentChance(myChance) && bulletsHandled == 0)
							playfieldObject = this.agent.movement.FindAimTarget(specialAbility, bulletType);

						autoAimApplied = true;
					}

					if (playfieldObject != null)
						bullet.movement.AutoAim(this.agent, playfieldObject, bullet);

					bool noRecoil = true;

					if (myWeapon != null && myWeapon.rapidFire && (this.heldFire || this.timeSinceLastBulletSpawn < 0.4f))
						noRecoil = false;

					if (!noRecoil)
					{
						if (recoilEffect == 999f)
						{
							if (agentAccuracy + moreAccuracy <= 0)
								recoilEffect = (float)UnityEngine.Random.Range(-20, 20);
							else if (agentAccuracy + moreAccuracy == 1)
								recoilEffect = (float)UnityEngine.Random.Range(-10, 10);
							else if (agentAccuracy + moreAccuracy == 2)
								recoilEffect = (float)UnityEngine.Random.Range(-5, 5);
							else
								recoilEffect = 0f;
						}

						bullet.movement.RotateToAngleTransform(bullet.transform.eulerAngles.z + recoilEffect);

						if (bulletType != bulletStatus.Shotgun)
							this.agent.objectMult.MultAttackProjectile(playfieldObject, bullet, specialAbility, isSilenced, isRubberBullet, (int)recoilEffect);
						else
							shotgunFlag = true;
					}
					else if (bulletType != bulletStatus.Shotgun)
						this.agent.objectMult.MultAttackProjectile(playfieldObject, bullet, specialAbility, isSilenced, isRubberBullet, 0);
					else
						shotgunFlag = true;
				}
				else if ((this.agent.isPlayer > 0 && !this.agent.outOfControl && !this.agent.localPlayer) || (!this.gc.serverPlayer && !this.agent.localPlayer))
				{
					if (this.agent.melee.attackObject != null)
					{
						if (this.agent.isPlayer > 0)
							bullet.movement.AutoAim(this.agent, this.agent.melee.attackObject, bullet);
						else
						{
							bullet.movement.RotateToObjectOffsetTr(this.agent.melee.attackObject.go);

							if (this.clientNPCAngle != 0)
							{
								bullet.movement.AutoAim(this.agent, this.agent.melee.attackObject, bullet);
								bullet.movement.RotateToAngleTransform(bullet.transform.eulerAngles.z + (float)this.clientNPCAngle);
							}
						}
					}
					else if (this.agent.oma.mindControlled && this.agent.mindControlAgent == this.gc.playerAgent && !this.gc.serverPlayer)
					{
						int myChance2 = (agentAccuracy + 1 + moreAccuracy) * 25;
						playfieldObject = null;

						if (this.gc.percentChance(myChance2) && bulletsHandled == 0)
							playfieldObject = this.agent.movement.FindAimTarget(specialAbility, bulletType);

						if (playfieldObject != null)
							bullet.movement.AutoAim(this.agent, playfieldObject, bullet);
						else
							bullet.movement.RotateToAngleTransform(this.agent.tr.eulerAngles.z - 90f);

						if (bulletType != bulletStatus.Shotgun)
							this.agent.objectMult.MultAttackProjectile(playfieldObject, bullet, specialAbility, isSilenced, isRubberBullet, (int)recoilEffect);
						else
							shotgunFlag = true;
					}
					else if (bulletType == bulletStatus.Shotgun)
						bullet.movement.RotateToPositionOffsetTr(this.agent.melee.attackPositionList[bulletsHandled]);
					else
						bullet.movement.RotateToPositionOffsetTr(this.agent.melee.attackPosition);
				}
				else
				{
					int npcAccuracyBonus = 25; // num6

					if (this.agent.opponent != null && !this.agent.oma.mindControlled)
					{
						bullet.movement.RotateToObjectOffsetTr(this.agent.opponent.go);

						if (this.agent.opponent.isPlayer > 0)
						{
							if (this.agent.opponent.statusEffects.hasTrait("HardToShoot"))
							{
								moreAccuracy -= 2;
							}
							else if (this.agent.opponent.statusEffects.hasTrait("HardToShoot2"))
							{
								moreAccuracy -= 2;
								npcAccuracyBonus = 15;
							}
						}
					}
					else if (this.agent.oma.mindControlled)
					{
						if (this.agent.melee.attackObject != null)
						{
							bullet.movement.RotateToObjectOffsetTr(this.agent.melee.attackObject.go);

							if (this.clientNPCAngle != 0)
							{
								bullet.movement.AutoAim(this.agent, this.agent.melee.attackObject, bullet);
								bullet.movement.RotateToAngleTransform(bullet.transform.eulerAngles.z + (float)this.clientNPCAngle);
							}
						}
						else
						{
							playfieldObject = null;
							int netAccuracy = (agentAccuracy + 1 + moreAccuracy) * 25;

							if (this.gc.percentChance(netAccuracy) && bulletsHandled == 0)
								playfieldObject = this.agent.movement.FindAimTarget(specialAbility, bulletType);

							if (playfieldObject != null)
								bullet.movement.AutoAim(this.agent, playfieldObject, bullet);
							else
								bullet.movement.RotateToAngleTransform(this.agent.tr.eulerAngles.z - 90f);
						}
					}
					else
						bullet.movement.RotateToAngleTransform(this.agent.tr.eulerAngles.z);

					if (!autoAimApplied)
					{
						if (((this.gc.percentChance(Mathf.Clamp(agentAccuracy + 1 + moreAccuracy, 1, 10) * npcAccuracyBonus) && bulletsHandled == 0) || agentAccuracy == 90) && agentAccuracy != -90)
							autoAim = true;

						autoAimApplied = true;
					}

					if (autoAim)
					{
						playfieldObject = this.agent.opponent;

						if (bulletType != bulletStatus.Shotgun)
							this.agent.objectMult.MultAttackProjectile(playfieldObject, bullet, specialAbility, isSilenced, isRubberBullet, 0);
						else
							shotgunFlag = true;
					}
					else if (!autoAim)
					{
						if (num2 == 999f)
						{
							if (this.agent.opponent != null)
								bullet.movement.AutoAim(this.agent, this.agent.opponent, bullet);

							if (this.gc.percentChance(50))
								num2 = (float)UnityEngine.Random.Range(-10, -20);
							else
								num2 = (float)UnityEngine.Random.Range(10, 20);
						}

						bullet.movement.RotateToAngleTransform(bullet.transform.eulerAngles.z + num2);

						if (bulletType != bulletStatus.Shotgun)
							this.agent.objectMult.MultAttackProjectile(this.agent.opponent, bullet, specialAbility, isSilenced, isRubberBullet, (int)num2);
						else
							shotgunFlag = true;
					}
				}

				float z = bullet.tr.eulerAngles.z;
				float num7 = (float)((4 - (agentAccuracy + 1 + moreAccuracy)) * 4);
				UnityEngine.Random.Range(-num7, num7);

				if (this.agent.killerRobot || this.agent.oma.mindControlled)
					Quaternion rotation = bullet.tr.rotation;

				if (bulletType == bulletStatus.Shotgun)
				{
					float zAngle = bullet.tr.eulerAngles.z;

					if (bulletsHandled == 0)
						zAngle += (float)UnityEngine.Random.Range(-2, 2);
					else if (bulletsHandled == 1)
						zAngle += (float)UnityEngine.Random.Range(-4, -2);
					else if (bulletsHandled == 2)
						zAngle += (float)UnityEngine.Random.Range(2, 4);

					bullet.movement.RotateToAngleTransform(zAngle);
					list.Add(bullet);
				}

				bullet.movement.MoveForwardTransform(0.32f);
				bulletsHandled++;
				continue;
			}

			if (bulletType == bulletStatus.Shotgun && shotgunFlag)
				this.agent.objectMult.MultAttackProjectileMore(playfieldObject, list, isSilenced, isRubberBullet, specialAbility);

			if (this.agent.agentSpriteTransform.localScale.x < 1f)
			{
				bullet.dirHelper.localPosition = new Vector3(0f, 0.16f, 0f);
				GameObject gameObject = this.gc.tileInfo.IsOverlapping(bullet.dirHelper.position, "Wall");

				if (gameObject != null)
				{
					if (list.Count > 0)
					{
						using (List<Bullet>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Bullet bullet2 = enumerator.Current;
								bullet2.bulletCollider.CollideWithWall(gameObject.GetComponent<BoxCollider2D>());
							}

							goto IL_BDD;
						}
					}

					bullet.bulletCollider.myBullet = bullet;
					bullet.bulletCollider.CollideWithWall(gameObject.GetComponent<BoxCollider2D>());
				}
			}

		IL_BDD:

			this.timeSinceLastBulletSpawn = 0f;
			return bullet;
		}
	}
}