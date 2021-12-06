import {State} from "../typings/redux"

export const getUser = (state: State) => state.authentication.user
