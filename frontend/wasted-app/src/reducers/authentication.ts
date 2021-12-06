import {User} from "@react-native-google-signin/google-signin"
import {createReducer} from "@reduxjs/toolkit"
import {setUser} from "../actions/authentication"

interface State {
  user?: User
}

export const initialState: State = {
  user: undefined
}

export const authentication = createReducer(initialState, builder => {
  builder.addCase(setUser, (state, {payload}) => {
    state.user = payload
  })
})
