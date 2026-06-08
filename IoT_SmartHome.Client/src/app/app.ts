import { Component, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
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
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.fetchDashboardData();
  }

  // fetches data from the .NET API
  // "subscribe" is equivalent to Task and await in C#
  fetchDashboardData() {
    this.isLoading = true;

    // Fire all API requests in parallel using RxJS forkJoin
    forkJoin({
      lights: this.http.get<SmartLight[]>(`${this.baseUrl}/smartlights`),
      thermostats: this.http.get<SmartThermostat[]>(`${this.baseUrl}/smartthermostats`),
      cameras: this.http.get<SmartSecurityCamera[]>(`${this.baseUrl}/smartsecuritycameras`),
      speakers: this.http.get<SmartSpeaker[]>(`${this.baseUrl}/smartspeakers`),
      locks: this.http.get<SmartLock[]>(`${this.baseUrl}/smartlocks`)
    }).subscribe({
      next: (response) => {
        this.lights = response.lights;
        this.thermostats = response.thermostats;
        this.cameras = response.cameras;
        this.speakers = response.speakers;
        this.locks = response.locks;
      },
      error: (err) => {
        console.error('Failed to load dashboard devices:', err);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
