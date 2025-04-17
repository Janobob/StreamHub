import { Library } from './library.model';
import { LibraryRequest } from './library.request';
import { LibraryResponse } from './library.response';

export function toModel(response: LibraryResponse): Library {
  return {
    id: response.id,
    name: response.name,
    description: response.description,
    path: response.path,
  };
}

export function toRequest(model: Library): LibraryRequest {
  return {
    id: model.id,
    name: model.name,
    description: model.description,
    path: model.path,
  };
}
