import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React, {useState} from "react"
import {ScrollView} from "react-native"
import {Assets, Button, Card, Colors, Text, TextField, View} from "react-native-ui-lib"
import {convertPassword} from "../../../utils/credentials"
import {RootStackParamList} from "../../RootStackParamsList"

type restaurantRegistrationProps = NativeStackScreenProps<RootStackParamList, "RestaurantRegistration">

export const RestaurantRegistration = ({route, navigation}: restaurantRegistrationProps) => {
  const [name, setName] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [location, setLocation] = useState("")

  const finishRegistration = () => {
    if (password !== confirmPassword) {
      console.log("NO")
    }
  }

  return (
    <ScrollView>
      <View flexG center marginB-s10>
        <Text text20 marginV-s10>
          Registration
        </Text>
        <View centerV width={320}>
          <View>
            <TextField
              floatingPlaceholder
              autoCapitalize="none"
              underlineColor={Colors.blue60}
              placeholder="Restaurant Name"
              value={name}
              onChangeText={setName}
            />
          </View>
          <View marginB-s4>
            <TextField
              floatingPlaceholder
              autoCapitalize="none"
              underlineColor={Colors.blue60}
              placeholder="Email"
              value={email}
              onChangeText={setEmail}
            />
          </View>
          <View>
            <TextField
              floatingPlaceholder
              textContentType="password"
              autoCapitalize="none"
              underlineColor={Colors.blue60}
              placeholder="Password"
              value={convertPassword(password)}
              onChangeText={setPassword}
            />
          </View>
          <View marginB-s10>
            <TextField
              floatingPlaceholder
              autoCapitalize="none"
              underlineColor={Colors.blue60}
              placeholder="Confirm password"
              value={convertPassword(confirmPassword)}
              onChangeText={setConfirmPassword}
            />
            <Card padding-s3 backgroundColor={Colors.grey70} containerStyle={{borderColor: "red"}}>
              <Text text70L>Password should contain:</Text>
              <Text
                text80L
              >{`  ∙ at least 8 characters\n  ∙ 1 or more capital letters\n  ∙ 1 digit\n  ∙ 1 special character`}</Text>
            </Card>
          </View>
          <View marginB-s6 marginT-s4>
            <TextField
              autoCapitalize="none"
              underlineColor={Colors.blue60}
              placeholder="Location"
              value={location}
              rightButtonProps={{iconSource: Assets.icons.search}}
              onChangeText={setLocation}
            />
            {/* TODO: implement location picking */}
          </View>
        </View>
        <Button bg-blue40 label="Register" onPress={finishRegistration} />
      </View>
    </ScrollView>
  )
}
