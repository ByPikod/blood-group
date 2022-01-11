using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodGroup
{
    public partial class BloodGroup : Form
    {
        public BloodGroup()
        {
            
            InitializeComponent();
            Blood.createBloodGroups();
            comboBox1.SelectedIndex = 0;

        }

        private void updateList(object sender, EventArgs e)
        {
            
            BloodType type;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    type = BloodType.A;
                    break;
                case 1:
                    type = BloodType.B;
                    break;
                case 2:
                    type = BloodType.AB;
                    break;
                case 3:
                    type = BloodType.ZERO;
                    break;
                default:
                    type = BloodType.A;
                    break;
            }
            Rh rh = Rh.NEGATIVE;
            if (radioButton1.Checked) rh = Rh.POSITIVE;

            Blood blood = Blood.getBlood(type, rh);

            listBox1.Items.Clear();
            foreach (Blood canTake in blood.canTake())
            {
                listBox1.Items.Add(canTake.ToString());
            }

            listBox2.Items.Clear();
            foreach (Blood canGive in blood.CanGive())
            {
                listBox2.Items.Add(canGive.ToString());
            }


        }

    }

    public enum BloodType
    {
        A,
        B,
        AB,
        ZERO
    }

    public enum Rh
    {
        POSITIVE,
        NEGATIVE
    }

    public class Blood
    {

        public static List<Blood> bloodGroups = new List<Blood>();

        BloodType type;
        Rh rh;

        public Blood(BloodType type, Rh rh)
        {

            this.type = type;
            this.rh = rh;

        }

        public List<Blood> canTake()
        {

            List<Blood> ret = new List<Blood>();
            ret.Add(getBlood(type, rh));

            if (type == BloodType.ZERO)
            {
                if (rh == Rh.POSITIVE)
                    ret.Add(getBlood(type, Rh.NEGATIVE));
            }else if (type == BloodType.AB)
            {

                ret.Add(getBlood(BloodType.A, rh));
                ret.Add(getBlood(BloodType.B, rh));
                ret.Add(getBlood(BloodType.ZERO, rh));
                if (rh == Rh.POSITIVE)
                {

                    ret.Add(getBlood(BloodType.A, Rh.NEGATIVE));
                    ret.Add(getBlood(BloodType.B, Rh.NEGATIVE));
                    ret.Add(getBlood(BloodType.AB, Rh.NEGATIVE));
                    ret.Add(getBlood(BloodType.ZERO, Rh.NEGATIVE));

                }

            }
            else
            {
                ret.Add(getBlood(BloodType.ZERO, Rh.NEGATIVE));

                if (rh == Rh.POSITIVE)
                {
                    ret.Add(getBlood(BloodType.ZERO, Rh.POSITIVE));
                    ret.Add(getBlood(type, Rh.NEGATIVE));
                }
            }

            return ret;

        }

        public List<Blood> CanGive()
        {

            List<Blood> ret = new List<Blood>();

            ret.Add(getBlood(type, rh));

            if (type == BloodType.AB) {
                if (rh == Rh.NEGATIVE)
                    ret.Add(getBlood(type, Rh.POSITIVE));
            }
            else if (type == BloodType.ZERO)
            {

                ret.Add(getBlood(BloodType.A, rh));
                ret.Add(getBlood(BloodType.B, rh));
                ret.Add(getBlood(BloodType.AB, rh));

                if (rh == Rh.NEGATIVE)
                {

                    ret.Add(getBlood(BloodType.A, Rh.POSITIVE));
                    ret.Add(getBlood(BloodType.B, Rh.POSITIVE));
                    ret.Add(getBlood(BloodType.AB, Rh.POSITIVE));
                    ret.Add(getBlood(BloodType.ZERO, Rh.POSITIVE));

                }

            }
            else
            {

                ret.Add(getBlood(BloodType.AB, rh));
                if (rh == Rh.NEGATIVE)
                {

                    ret.Add(getBlood(type, Rh.POSITIVE));
                    ret.Add(getBlood(BloodType.AB, Rh.POSITIVE));

                }

            }

            return ret;

        }

        public static Blood getBlood(BloodType type, Rh rh)
        {
            
            foreach (Blood blood in bloodGroups)
            {
                if(blood.type == type && blood.rh == rh) return blood;
            }
            return null;

        }

        public override string ToString()
        {

            string ret = "";
            switch (type)
            {
                case BloodType.A:
                    ret += "A";
                    break;
                case BloodType.B:
                    ret += "B";
                    break;
                case BloodType.AB:
                    ret += "AB";
                    break;
                case BloodType.ZERO:
                    ret += "0";
                    break;
            }
            if (rh == Rh.POSITIVE) ret += "+";
            else
                ret += "-";
            return ret;

        }

        public static void createBloodGroups()
        {

            bloodGroups.Add(new Blood(BloodType.A, Rh.POSITIVE));
            bloodGroups.Add(new Blood(BloodType.A, Rh.NEGATIVE));
            bloodGroups.Add(new Blood(BloodType.B, Rh.POSITIVE));
            bloodGroups.Add(new Blood(BloodType.B, Rh.NEGATIVE));
            bloodGroups.Add(new Blood(BloodType.AB, Rh.POSITIVE));
            bloodGroups.Add(new Blood(BloodType.AB, Rh.NEGATIVE));
            bloodGroups.Add(new Blood(BloodType.ZERO, Rh.POSITIVE));
            bloodGroups.Add(new Blood(BloodType.ZERO, Rh.NEGATIVE));

        }

    }
}
