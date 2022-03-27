import { ParticipantDto } from './web-api.model';

export interface ParticipantChange {
  sessionId: string;
  participant: ParticipantDto;
}
