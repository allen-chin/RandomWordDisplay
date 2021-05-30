export class ServerState {
  constructor(
    public commandRunning: boolean,
    public commandTimeRemaining: number,
    public currentWordSelected: string
  ) { }
}
