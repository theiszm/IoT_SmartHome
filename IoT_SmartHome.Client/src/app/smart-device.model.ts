export interface SmartDevice {
	id: number;
	room: string;
	isOnline: boolean;
	lastUpdated: string; // ISO Date String from .NET
}

export enum SystemMode {
  Off = 'Off',
  Heat = 'Heat',
  Cool = 'Cool',
  Eco = 'Eco'
}

export interface SmartLight extends SmartDevice {
  isOn: boolean;
  brightness: number;
  maxWattage: number;
  currentPowerUsage: number; // calculated value to be sent from the backend
}

export interface SmartThermostat extends SmartDevice {
	currentTemperature: number;
	targetTemperature: number;
	systemMode: SystemMode; // uses the strict Enum options
}

export interface SmartSecurityCamera extends SmartDevice {
  isRecording: boolean;
  storageUsagePercentage: number;
  motionDetected: boolean;
}

export interface SmartSpeaker extends SmartDevice {
  volume: number;
  currentTrack: string;
  isMuted: boolean;
}

export interface SmartLock extends SmartDevice {
  isLocked: boolean;
  batteryPercentage: number;
  lastUnlockedTime: string;
}


