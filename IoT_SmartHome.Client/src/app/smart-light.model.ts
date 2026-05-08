export interface SmartLight {
  id: number;
  room: string;
  isOn: boolean;
  brightness: number;
  maxWattage: number;
  currentPowerUsage number; // calculated value to be sent from the backend
}
