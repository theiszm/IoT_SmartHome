import { Component, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SmartLight, SmartThermostat, SmartSecurityCamera, SmartSpeaker, SmartLock } from './smart-device.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})

export class AppComponent implements OnInit {
  protected readonly title = signal('IoT_SmartHome.Client');

  public lights: SmartLight[] = [];
  public thermostats: SmartThermostat[] = [];
  public cameras: SmartSecurityCamera[] = [];
  public speakers: SmartSpeaker[] = [];
  public locks: SmartLock[] = [];

  public isLoading: boolean = true;
  private readonly baseUrl = 'https://localhost:7016/api';

  // inject HttpClient into constructor
  constructor(private http: HttpClient) {}
  
  ngOnInit() {
	  this.fetchAllDevices();
  }

  // fetches data from the .NET API
  // "subscribe" is equivalent to Task and await in C#
  fetchAllDevices() {
    this.isLoading = true;
    console.log("Fetching all smart home devices...");

    this.getLights();
    this.getThermostats();
    this.getCameras();
    this.getSpeakers();
    this.getLocks();
  }

  getLights() {
    this.http.get<SmartLight[]>('{this.baseUrl}/smartlights').subscribe({
      next: (result) => {
        this.lights = result; this.checkLoadingStatus();
      },
      error: (err) => { console.error('Lights API Error:', err); this.checkLoadingStatus() }
    });
  }

  getThermostats() {
    this.http.get<SmartThermostat[]>(`${this.baseUrl}/smartthermostats`).subscribe({
      next: (result) => { this.thermostats = result; this.checkLoadingStatus(); },
      error: (err) => { console.error('Thermostats API Error:', err); this.checkLoadingStatus(); }
    });
  }

  getCameras() {
    this.http.get<SmartSecurityCamera[]>(`${this.baseUrl}/smartsecuritycameras`).subscribe({
      next: (result) => { this.cameras = result; this.checkLoadingStatus(); },
      error: (err) => { console.error('Cameras API Error:', err); this.checkLoadingStatus(); }
    });
  }

  getSpeakers() {
    this.http.get<SmartSpeaker[]>(`${this.baseUrl}/smartspeakers`).subscribe({
      next: (result) => { this.speakers = result; this.checkLoadingStatus(); },
      error: (err) => { console.error('Speakers API Error:', err); this.checkLoadingStatus(); }
    });
  }

  getLocks() {
    this.http.get<SmartLock[]>(`${this.baseUrl}/smartlocks`).subscribe({
      next: (result) => { this.locks = result; this.checkLoadingStatus(); },
      error: (err) => { console.error('Locks API Error:', err); this.checkLoadingStatus(); }
    });
  }

  // turn off spinner once loading finishes or fails
  private checkLoadingStatus() {
    this.isLoading = false;
  }
	  
}
