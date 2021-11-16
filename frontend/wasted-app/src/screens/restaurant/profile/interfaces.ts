import {NavigationComponentProps} from "react-native-navigation"

export interface ProfileOwnProps {
    restaurantId: string
}

export interface ProfileProps extends NavigationComponentProps, ProfileOwnProps {}
