import {User} from "@react-native-google-signin/google-signin"
import {useDispatch, useSelector} from "react-redux"
import {setUser} from "../actions/authentication"
import {getUser} from "../selectors/authentication"

export const useAuthentication = () => {
  const dispatch = useDispatch()

  return {
    user: useSelector(getUser),
    setUser: (user: User) => dispatch(setUser(user))
  }
}
