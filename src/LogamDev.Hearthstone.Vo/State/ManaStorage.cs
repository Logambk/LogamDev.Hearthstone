using System;

namespace LogamDev.Hearthstone.Vo.State
{
    public class ManaStorage
    {
        //TODO: implement overloading mechanism here
        private int permanentManaCrystalsSpentThisTurn;
        private int temporaryManaCrystalsAvailableThisTurn;

        public int PermanentManaCrystals { get; private set; }
        public int AvailableManaThisTurn => PermanentManaCrystals - permanentManaCrystalsSpentThisTurn + temporaryManaCrystalsAvailableThisTurn;

        public ManaStorage(int initialManaCrystals)
        {
            PermanentManaCrystals = initialManaCrystals;
        }

        public void RefreshPermanentManaCrystals()
        {
            permanentManaCrystalsSpentThisTurn = 0;
        }

        public void BurnTemporaryCrystals()
        {
            temporaryManaCrystalsAvailableThisTurn = 0;
        }

        public void SpendMana(int mana)
        {
            if (mana < 0)
            {
                throw new ArgumentException("Trying to spend negative amount of mana");
            }

            if (mana > AvailableManaThisTurn)
            {
                throw new ArgumentException($"Trying to spend {mana} while only {AvailableManaThisTurn} available");
            }

            if (mana <= temporaryManaCrystalsAvailableThisTurn)
            {
                temporaryManaCrystalsAvailableThisTurn -= mana;
            }
            else
            {
                mana -= temporaryManaCrystalsAvailableThisTurn;
                temporaryManaCrystalsAvailableThisTurn = 0;
                permanentManaCrystalsSpentThisTurn += mana;
            }
        }

        public void AddManaCrystals(int manaCrystals, bool isEmpty = true)
        {
            if (manaCrystals < 0)
            {
                throw new ArgumentException("Trying to add negative amount of mana crystals");
            }

            PermanentManaCrystals += manaCrystals;
            if (isEmpty)
            {
                permanentManaCrystalsSpentThisTurn += manaCrystals;
            }
        }

        public void DestroyManaCrystals(int manaCrystals)
        {
            if (manaCrystals < 0)
            {
                throw new ArgumentException("Trying to destroy negative amount of mana crystals");
            }

            if (manaCrystals > PermanentManaCrystals)
            {
                throw new ArgumentException($"Trying to destroy {manaCrystals} mana crysltals, but only {PermanentManaCrystals} available");
            }

            PermanentManaCrystals -= manaCrystals;
            if (manaCrystals <= permanentManaCrystalsSpentThisTurn)
            {
                permanentManaCrystalsSpentThisTurn -= manaCrystals;
            }
            else
            {
                permanentManaCrystalsSpentThisTurn = 0;
            }
        }
    }
}
