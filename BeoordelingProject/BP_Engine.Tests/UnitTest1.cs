﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeoordelingProject.Engine;
using System.Collections.Generic;

namespace BeoordelingProject.Tests {
    [TestClass]
    public class BeoordelingsEngineUT {
        List<double> middens = new List<double>();
        List<int> wegingen = new List<int>();
        IBeoordelingsEngine engine = new BeoordelingsEngine();
        double midden1 = 12.5; // RV
        double midden2 = 8; // OV
        double res1, res2;

        public BeoordelingsEngineUT() {
            wegingen.Add(3);
            wegingen.Add(3);
            wegingen.Add(2);
            wegingen.Add(2);
            wegingen.Add(1);
            wegingen.Add(2);
            wegingen.Add(3);
        }

        [TestMethod]
        public void deelaspectTest() {
            res1 = engine.deelaspect(midden1, wegingen[0]);
            res2 = engine.deelaspect(midden2, wegingen[1]);

            Assert.AreEqual(res1, 37.5);
            Assert.AreEqual(res2, 24);
        }

        [TestMethod]
        public void totaalDeelaspectTest() {
            middens.Add(midden1);
            middens.Add(midden2);

            double res = engine.totaalDeelaspect(middens, wegingen);

            Assert.AreEqual(res, 61.5);
        }

        [TestMethod]
        public void totaalWegingTest() {
            double res = engine.totaalWeging(wegingen);

            Assert.AreEqual(res, 16);
        }

        [TestMethod]
        public void totaalScore() {
            middens.Add(midden1);
            middens.Add(midden2);

            double res = engine.totaalScore(middens, wegingen);

            Assert.AreEqual(res, 3.8438);
        }

        // Volgende testen zijn gebaseerd op "Rekenblad bij eindbeoordeling BP 2013-2014 BAKO.xlsx"
        // Eindbeoordelingen hebben een weging over een volledig aspect.
        // Tussentijdse beoordelingen hebben over elke deelaspect een weging. (zie boven)

        [TestMethod]
        public void EindScorePromotor() {
            // Eindbeoordeling waarbij alleen de promotor op 1 aspect beoordeelt.
            // Voorbeeld: Attitudes
            // Score op 40

            wegingen = new List<int>() {
                2
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Deze voorbeeld: Max = (3x 20) = 60 op 40 = 1.5
            double eindRes = (resPromotor) / 1.5;

            Assert.AreEqual(eindRes, 16);
        }

        [TestMethod]
        public void EindScorePromotorTweedelezer() {
            // Eindbeoordeling waarbij de promotor en tweede lezer op 1 aspect beoordelen.
            // Voorbeeld: Vormtechnische aspecten
            // Score op 40

            wegingen = new List<int>() {
                2
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // 2de lezer (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Deze voorbeeld: Max = (6x 20) = 120 op 40 = 3
            double eindRes = (resPromotor + resTweedelezer) / 3;

            Assert.AreEqual(eindRes, 16);
        }

        [TestMethod]
        public void EindScorePromotorTweedelezerCriticalFriend() {
            // Eindbeoordeling waarbij de promotor, tweede lezer en critical friend (cf) op 1 aspect beoordelen.
            // Voorbeeld 1: Inhoud en opbouw argumentatie
            // Voorbeeld 2: Praktische relevantie en/of realisaties
            // Beide scores op 60

            wegingen = new List<int>() {
                3
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // 2de lezer (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Critical Friend (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resCriticalFriend = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Voorbeeld 1: Max = (9x 20) = 180 op 60 = 3
            // Voorbeeld 2: Max = (6x 20) = 120 op 60 = 2
            double eindRes1 = (resPromotor + resTweedelezer + resCriticalFriend) / 3;

            Assert.AreEqual(eindRes1, 24);
        }
    }
}
