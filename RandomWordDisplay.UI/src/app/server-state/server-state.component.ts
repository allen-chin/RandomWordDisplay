import { Component, OnInit } from '@angular/core';
import { timer } from 'rxjs';
import { switchMap, retry, share, takeUntil } from 'rxjs/operators';
import { ServerState } from '../server-state.model';
import { ServerStateService } from '../server-state.service';


@Component({
  selector: 'app-server-state',
  templateUrl: './server-state.component.html',
  styleUrls: ['./server-state.component.css']
})
export class ServerStateComponent implements OnInit {
  serverState: ServerState = new ServerState(false, 0, '');

  constructor(private serverStateService: ServerStateService) { }

  ngOnInit(): void {
    this.getServerState();
  }

  getServerState(): void {
    timer(1, 1000).pipe(
      switchMap(() => this.serverStateService.getServerState())
    ).subscribe(serverState => this.serverState = serverState);
  }
}
