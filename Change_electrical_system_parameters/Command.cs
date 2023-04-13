using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Electrical;
using System.Collections.ObjectModel;
using Autodesk.Revit.DB.Structure;

namespace Change_electrical_system_parameters
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {
        public static bool ui_approve = new bool();
        public static string last_method = new string(new char[] { });
        public static string protection_type = new string(new char[] { });
        public static double voltage_loss = new double();
        public static string laying_method = new string(new char[] { });

        public Result Execute(ExternalCommandData command_data, ref string message, ElementSet elements)
        {
            UIApplication ui_application = command_data.Application;
            Document document = ui_application.ActiveUIDocument.Document;

            bool? viewer = new Viewer(document).ShowDialog();

            if (ui_approve && last_method.ToString() == "Branch circuit parameters")
            {
                document = ui_application.ActiveUIDocument.Document;

                FilteredElementCollector electrical_systems = new FilteredElementCollector(document).OfClass(typeof(ElectricalSystem));
                FilteredElementCollector wire_types = new FilteredElementCollector(document).OfClass(typeof(WireType));
                Dictionary<ElementId, double> cable_sizes = new Dictionary<ElementId, double>();
                Dictionary<string, Dictionary<ElementId, int>> circuit_number = new Dictionary<string, Dictionary<ElementId, int>>();
                Dictionary<ElementId, string> protection_device = new Dictionary<ElementId, string>();

                using (Transaction transaction = new Transaction(document, "Change branch circuit parameters value"))
                {
                    transaction.Start();

                    foreach (ElectricalSystem electrical_system in electrical_systems)
                    {
                        // Set wire type for electrical systems
                        if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 25 && Voltage_loss(electrical_system, 2.5, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 2.5;}
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 35 && Voltage_loss(electrical_system, 4.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 4.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 42 && Voltage_loss(electrical_system, 6.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 6.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 55 && Voltage_loss(electrical_system, 10.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 10.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 75 && Voltage_loss(electrical_system, 16.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 16.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 95 && Voltage_loss(electrical_system, 25.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 25.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 120 && Voltage_loss(electrical_system, 35.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 35.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 145 && Voltage_loss(electrical_system, 50.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 50.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 180 && Voltage_loss(electrical_system, 70.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 70.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 220 && Voltage_loss(electrical_system, 95.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 95.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 260 && Voltage_loss(electrical_system, 120.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 120.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 305 && Voltage_loss(electrical_system, 150.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 150.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Медь" && electrical_system.ApparentCurrent < 350 && Voltage_loss(electrical_system, 185.0, 0.0178) < voltage_loss) { cable_sizes[electrical_system.Id] = 185.0; }

                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 19 && Voltage_loss(electrical_system, 2.5, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 2.5; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 27 && Voltage_loss(electrical_system, 4.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 4.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 32 && Voltage_loss(electrical_system, 6.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 6.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 42 && Voltage_loss(electrical_system, 10.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 10.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 60 && Voltage_loss(electrical_system, 16.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 16.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 75 && Voltage_loss(electrical_system, 25.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 25.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 90 && Voltage_loss(electrical_system, 35.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 35.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 110 && Voltage_loss(electrical_system, 50.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 50.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 140 && Voltage_loss(electrical_system, 70.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 70.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 170 && Voltage_loss(electrical_system, 95.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 95.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 200 && Voltage_loss(electrical_system, 120.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 120.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 235 && Voltage_loss(electrical_system, 150.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 150.0; }
                        else if (electrical_system.WireType.WireMaterial.Name == "Алюминий" && electrical_system.ApparentCurrent < 270 && Voltage_loss(electrical_system, 185.0, 0.02994) < voltage_loss) { cable_sizes[electrical_system.Id] = 185.0; }

                        string cable_section = new string(new char[] { });

                        try
                        {
                            if (cable_sizes[electrical_system.Id].ToString().Contains(",5"))
                            {
                                cable_section = Math.Round(cable_sizes[electrical_system.Id], 1).ToString();
                            }
                            else
                            {
                                cable_section = Math.Round(cable_sizes[electrical_system.Id]).ToString();
                            }
                        }
                        catch
                        {
                            TaskDialog.Show("Error", "В шаблоне отсутствует кабель: " + electrical_system.WireType.Name + " который находится в принципиальной схеме " + electrical_system.Name + "!\nРекомендуется добавить кабель в шаблон и перезапустить плагин!");
                        }

                        foreach (WireType wire_type in wire_types)
                        {
                            if (electrical_system.WireType.Name.Split(' ')[0] + " " + (electrical_system.PolesNumber + 2) + "x" + cable_section == wire_type.Name)
                            {
                                electrical_system.WireType = wire_type;
                            }
                        }

                        // Set cable name to electrical systems for specification
                        electrical_system.LookupParameter("Наименование").Set(electrical_system.WireType.LookupParameter("BI_наименование").AsString());

                        // Set cable supplier to electrical systems for specification
                        electrical_system.LookupParameter("Поставщик").Set(electrical_system.WireType.LookupParameter("BI_завод_изготовитель").AsString());

                        // Set cable unit to electrical systems for specification
                        electrical_system.LookupParameter("Единица измерения").Set(electrical_system.WireType.LookupParameter("BI_единица_измерения").AsString());

                        // Set cable weight to electrical systems for specification
                        electrical_system.LookupParameter("Масса").Set(electrical_system.WireType.LookupParameter("BI_масса").AsDouble());

                        // Set circuit number
                        if (!circuit_number.ContainsKey(electrical_system.PanelName))
                        {
                            circuit_number[electrical_system.PanelName] = new Dictionary<ElementId, int>();

                            circuit_number[electrical_system.PanelName].Add(electrical_system.Id, electrical_system.StartSlot);
                        }
                        else
                        {
                            circuit_number[electrical_system.PanelName].Add(electrical_system.Id, electrical_system.StartSlot);
                        }

                        // Set protection device for electrical systems
                        if (electrical_system.LoadClassifications.ToString() == "Розеточная сеть" && electrical_system.ApparentCurrent < 20.0) { protection_device[electrical_system.Id] = "АВДТ3" + (electrical_system.PolesNumber + 1) + " " + protection_type + "25 30 мА"; }
	                    else if (electrical_system.LoadClassifications.ToString() == "Розеточная сеть" && electrical_system.ApparentCurrent < 25.0) { protection_device[electrical_system.Id] = "АВДТ3" + (electrical_system.PolesNumber + 1) + " " + protection_type + "32 30 мА"; }
                        else if (electrical_system.LoadClassifications.ToString() == "Розеточная сеть" && electrical_system.ApparentCurrent < 32.0) { protection_device[electrical_system.Id] = "АВДТ3" + (electrical_system.PolesNumber + 1) + " " + protection_type + "40 30 мА"; }
                        else if (electrical_system.LoadClassifications.ToString() == "Розеточная сеть" && electrical_system.ApparentCurrent < 40.0) { protection_device[electrical_system.Id] = "АВДТ3" + (electrical_system.PolesNumber + 1) + " " + protection_type + "50 100 мА"; }
	                    else if (electrical_system.LoadClassifications.ToString() == "Розеточная сеть" && electrical_system.ApparentCurrent < 50.0) { protection_device[electrical_system.Id] = "АВДТ3" + (electrical_system.PolesNumber + 1) + " " + protection_type + "63 100 мА"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 25.0 && cable_sizes[electrical_system.Id] == 2.5) { protection_device[electrical_system.Id] = "ВА47-29-" + electrical_system.PolesNumber + " " + protection_type + "16"; }
                        else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 35.0 && cable_sizes[electrical_system.Id] == 4.0) { protection_device[electrical_system.Id] = "ВА47-29-" + electrical_system.PolesNumber + " " + protection_type + "25"; }
                        else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 42.0 && cable_sizes[electrical_system.Id] == 6.0) { protection_device[electrical_system.Id] = "ВА47-29-" + electrical_system.PolesNumber + " " + protection_type + "32"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 55.0 && cable_sizes[electrical_system.Id] == 10.0) { protection_device[electrical_system.Id] = "ВА47-29-" + electrical_system.PolesNumber + " " + protection_type + "40"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 75.0 && cable_sizes[electrical_system.Id] == 16.0) { protection_device[electrical_system.Id] = "ВА47-29-" + electrical_system.PolesNumber + " " + protection_type + "50"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 95.0 && cable_sizes[electrical_system.Id] == 25.0) { protection_device[electrical_system.Id] = "ВА47-29-" + electrical_system.PolesNumber + " " + protection_type + "63"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 120.0 && cable_sizes[electrical_system.Id] == 35.0) { protection_device[electrical_system.Id] = "ВА47-100-" + electrical_system.PolesNumber + " " + protection_type + "80"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 145.0 && cable_sizes[electrical_system.Id] == 50.0) { protection_device[electrical_system.Id] = "ВА47-100-" + electrical_system.PolesNumber + " " + protection_type + "100"; }
	                    else if (electrical_system.LoadClassifications.ToString() != "Розеточная сеть" && electrical_system.ApparentCurrent < 180.0 && cable_sizes[electrical_system.Id] == 70.0) { protection_device[electrical_system.Id] = "ВА47-150-" + electrical_system.PolesNumber + " " + protection_type + "125"; }

                        if (protection_device.ContainsKey(electrical_system.Id))
                        {
                            electrical_system.LookupParameter("Аппарат отходящей линии").Set(protection_device[electrical_system.Id]);
                        }

                        foreach (Element switch_board in new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_ElectricalEquipment).WhereElementIsNotElementType().Where(item => item.LookupParameter("Модуль 01") != null))
                        {
                            if (electrical_system.PanelName == switch_board.LookupParameter("Имя панели").AsString())
                            {
                                foreach (ElementType protection in new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_ElectricalEquipment).WhereElementIsElementType().Where(item => item.LookupParameter("Имя панели") == null))
                                {
                                    if (protection_device[electrical_system.Id] == protection.Name)
                                    {
                                        if (electrical_system.StartSlot + 3 < 10)
                                        {
                                            switch_board.LookupParameter("Модуль 0" + (electrical_system.StartSlot + 3)).Set(protection.Id);
                                        }
                                        else if (electrical_system.StartSlot + 3 > 9)
                                        {
                                            switch_board.LookupParameter("Модуль " + (electrical_system.StartSlot + 3)).Set(protection.Id);
                                        }
                                    }
                                }
                            }
                        }

                        // Set laying_method for electrical systems
                        electrical_system.LookupParameter("Способ прокладки").Set(laying_method);
                    }

                    transaction.Commit();
                }
            }
            else if (ui_approve && last_method.ToString() == "Main breaker parameters")
            {
                FilteredElementCollector electrical_equipments = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_ElectricalEquipment).WhereElementIsNotElementType();

                using (Transaction transaction = new Transaction(document, "Change main breaker parameters value"))
                {
                    transaction.Start();

                    foreach (Element electrical_equipment in new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_ElectricalEquipment).WhereElementIsNotElementType().Where(item => item.LookupParameter("Имя панели") != null && item.LookupParameter("Имя панели").AsString() != "" && item.Name != "Нет"))
                    {
                        if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 32.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(32.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("10"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }
                        else if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 40.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(40.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("10"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }
                        else if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 50.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(50.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("10"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }
                        else if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 63.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(63.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("15"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }
                        else if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 80.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(80.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("15"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }
                        else if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 100.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(100.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("15"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }
                        else if (electrical_equipment.LookupParameter("Ip (ток для расчетной нагрузки)").AsDouble() < 125.0) { electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").Set(125.0); electrical_equipment.LookupParameter("Ток короткого замыкания").Set("15"); electrical_equipment.LookupParameter("Вводной аппарат").Set("ВА47-" + electrical_equipment.LookupParameter("Ток короткого замыкания").AsString() + "0-" + electrical_equipment.LookupParameter("Количество фаз").AsInteger() + " " + protection_type + Math.Round(electrical_equipment.LookupParameter("Номинальный ток вводного аппарата").AsDouble())); }

                        electrical_equipment.LookupParameter("Уставка расцепителя").Set(5.0);

                        electrical_equipment.LookupParameter("Защитная характеристика").Set(protection_type);
                    }

                    transaction.Commit();
                }
            }
            else if (ui_approve && last_method.ToString() == "Switchgear parameters")
            {

            }
            else
            {
                return Result.Cancelled;
            }

            return Result.Succeeded;
        }

        public double Reactive_power(ElectricalSystem electrical_system)
        {
            double reactive_power = Math.Sqrt(Math.Pow(electrical_system.ApparentLoad, 2) - Math.Pow(electrical_system.TrueLoad, 2));

            return reactive_power;
        }

        public double Voltage_loss(ElectricalSystem electrical_system, double cable_size, double cable_resistivity)
        {
            double voltage_loss = new double();

            if (electrical_system.PolesNumber == 1)
            {
                voltage_loss = 2 * (electrical_system.TrueLoad * (cable_resistivity * (electrical_system.Length / 1000) / cable_size) + Reactive_power(electrical_system) * (2 * Math.PI * 50 * (0.005081 * electrical_system.Length * Math.Log(2 * electrical_system.Length / Math.Sqrt(4 * 2.5 / Math.PI)) - 0.75) * Math.Pow(10, -8)) / Math.Pow(220, 2) * 100);
            }
            else if (electrical_system.PolesNumber == 3)
            {
                voltage_loss = (electrical_system.TrueLoad * (cable_resistivity * (electrical_system.Length / 1000) / cable_size) + Reactive_power(electrical_system) * (2 * Math.PI * 50 * (0.005081 * electrical_system.Length * Math.Log(2 * electrical_system.Length / Math.Sqrt(4 * 4 / Math.PI)) - 0.75) * Math.Pow(10, -8))) / Math.Pow(380, 2) * 100;
            }

            return voltage_loss;
        }
    }
}