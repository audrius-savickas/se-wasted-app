module.exports = {
  env: {
    node: true,
    es6: true
  },
  settings: {
    react: {
      version: "detect"
    }
  },
  extends: ["eslint:recommended", "plugin:@typescript-eslint/recommended", "plugin:react/recommended"],
  plugins: ["react", "@typescript-eslint", "import", "prettier"],
  rules: {
    "@typescript-eslint/ban-ts-comment": "off",
    "@typescript-eslint/ban-types": "off",
    "@typescript-eslint/explicit-module-boundary-types": "off",
    "@typescript-eslint/no-empty-function": "off",
    "@typescript-eslint/no-empty-interface": "off",
    "@typescript-eslint/no-explicit-any": "off",
    "@typescript-eslint/no-inferrable-types": "off",
    "@typescript-eslint/no-unused-vars": "error",
    "@typescript-eslint/no-var-requires": "off",

    "react/display-name": "off",
    "react/jsx-boolean-value": "error",
    "react/jsx-sort-props": [
      "error",
      {
        callbacksLast: true,
        shorthandFirst: true,
        noSortAlphabetically: true
      }
    ],
    "react/jsx-tag-spacing": [
      "error",
      {
        beforeSelfClosing: "always",
        beforeClosing: "never"
      }
    ],
    "react/no-unescaped-entities": "off",
    "react/prop-types": "off",

    "no-async-promise-executor": "off",
    "no-case-declarations": "off",
    "sort-imports": ["error", {ignoreDeclarationSort: true}],

    "import/no-duplicates": "error",
    "import/order": [
      "error",
      {
        "newlines-between": "never",
        alphabetize: {
          order: "asc",
          caseInsensitive: true
        }
      }
    ],

    "prettier/prettier": [
      "error",
      {},
      {
        usePrettierrc: true
      }
    ]
  }
}
