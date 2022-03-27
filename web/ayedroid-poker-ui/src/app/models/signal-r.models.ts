import { ParticipantDto, TopicDto } from './web-api.model';

interface BaseNotification {
  sessionId: string;
}

export interface ParticipantNotification extends BaseNotification {
  participant: ParticipantDto;
}

export interface TopicNotification extends BaseNotification {
  topic: TopicDto;
}
